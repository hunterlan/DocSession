using System.ComponentModel.DataAnnotations;
using DocSession.Application;
using DocSession.Application.Appointments;
using DocSession.Entities.Requests;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseInMemoryDatabase("DocSession"));

builder.Services.AddScoped<IValidator<CreateAppointmentModel>, AppointmentValidator>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapPost("/users", (UserLoginModel login) =>
{

});
app.MapPost("/appointments/", async (IValidator<CreateAppointmentModel> validator,
  CreateAppointmentModel appointment) =>
{
  var validationResult = await validator.ValidateAsync(appointment);

  if (!validationResult.IsValid)
  {

  }

});

app.Run();
