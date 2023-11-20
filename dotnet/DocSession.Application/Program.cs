using System.ComponentModel.DataAnnotations;
using System.Data;
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
using Microsoft.AspNetCore.Authorization;
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

builder.Services.AddAuthorization();
builder.Services.AddControllers();

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

app.MapControllers();
app.Run();
