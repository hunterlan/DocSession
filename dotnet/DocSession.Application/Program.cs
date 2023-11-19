using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using DocSession.Application;
using DocSession.Application.Appointments;
using DocSession.Application.Models.Users.UserCreate;
using DocSession.Application.Models.Users.UserLogin;
using DocSession.Entities;
using DocSession.Entities.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(builder.Configuration.GetSection("ConnectionString").Value));
builder.Services.AddAuthentication(a =>
  {
    a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
  })
  .AddJwtBearer("jwt", o =>
  {
    o.TokenValidationParameters = new TokenValidationParameters()
    {
      ValidIssuer = config["Jwt:Issuer"],
      ValidAudience = config["Jwt:Audience"],
      IssuerSigningKey = new SymmetricSecurityKey(
      Encoding.UTF8.GetBytes(config["Jwt:Key"]!)),
      ValidateIssuer = true,
      ValidateAudience = true,
      ValidateLifetime = true,
      ValidateIssuerSigningKey = true
    };
  });

// Adding validators
builder.Services.AddScoped<IValidator<CreateAppointmentModel>, AppointmentValidator>();
builder.Services.AddScoped<IValidator<UserLoginModel>, UserValidator>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
  // use context
  dbContext.Database.EnsureCreated();
}

app.MapPost("/users", async (IValidator<UserLoginModel> validator, ApplicationContext db, UserLoginModel userLogin) =>
{
  var validationResult = await validator.ValidateAsync(userLogin);

  if (!validationResult.IsValid)
  {
    return Results.ValidationProblem(validationResult.ToDictionary());
  }

  var user = db.Users.FirstOrDefault(u => u.Person.Email == userLogin.Username && u.Password == userLogin.Password);

  if (user is null)
  {
    return Results.ValidationProblem(new Dictionary<string, string[]>
    {
      {"loginError", new string[] {"Login or password are incorrect!"}}
    });
  }

  var key = Encoding.ASCII.GetBytes(config["Jwt:Key"]!);
  var handler = new JsonWebTokenHandler();

  var claims = new List<Claim>
  {
    new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    new(JwtRegisteredClaimNames.Email, user.Person.Email)
  };

  var token = handler.CreateToken(new SecurityTokenDescriptor()
  {
    Subject = new ClaimsIdentity(claims),
    Expires = DateTime.UtcNow.AddHours(int.Parse(config["Jwt:TokenLifetime"]!)),
    Audience = config["Jwt:Audience"],
    Issuer = config["Jwt:Issuer"],
    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
  });

  return Results.Ok(token);
});
app.MapPost("/users/create", async (ApplicationContext db, UserCreateModel createUser) =>
{
  var newUser = new User(createUser);
  await db.Users.AddAsync(newUser);
  db.SaveChanges();
});
app.MapPost("/appointments/", async (IValidator<CreateAppointmentModel> validator, CreateAppointmentModel appointment,
  ApplicationContext db) =>
{
  var validationResult = await validator.ValidateAsync(appointment);

  if (!validationResult.IsValid)
  {
    return Results.ValidationProblem(validationResult.ToDictionary());
  }

  await db.Appointments.AddAsync(appointment.MapRequestToDomain());

  return Results.Ok();
});
//To-Do: search about can we get doctor id by jwt token
/*app.MapGet("/appointments/", async (int doctorId, int id, ApplicationContext db) =>
{
  var doctorsAppointments = db.Appointments.Where(a => a.DoctorId == doctorId);


});*/

app.Run();
