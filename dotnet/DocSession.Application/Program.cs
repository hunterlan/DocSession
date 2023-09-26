using DocSession.Application;
using DocSession.Application.Appointments;
using DocSession.Entities.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase("DocSession"));
builder.Services.AddAuthentication().AddJwtBearer();
builder.Services.AddScoped<IValidator<CreateAppointmentModel>, AppointmentValidator>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/users", (UserLoginModel login) =>
{

});
app.MapPost("/appointments/", async (IValidator<CreateAppointmentModel> validator,
  CreateAppointmentModel appointment, ApplicationContext db) =>
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
app.MapGet("/appointments/", async (int doctorId, int id, ApplicationContext db) =>
{
  var doctorsAppointments = db.Appointments.Where(a => a.DoctorId == doctorId);


});

app.Run();
