using DocSession.Application.Appointments;
using DocSession.Entities.Requests;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace DocSession.Application.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  [Authorize]
  public class AppointmentController(ApplicationContext context, IValidator<CreateAppointmentModel> validator) : ControllerBase
  {
    [HttpPost]
    [Authorize(Roles = "User, Doctor")]
    public async Task<IResult> CreateNewAppointment(CreateAppointmentModel appointment)
    {
      var validationResult = await validator.ValidateAsync(appointment);

      if (!validationResult.IsValid)
      {
        return Results.ValidationProblem(validationResult.ToDictionary());
      }

      var newAppointment = appointment.MapRequestToDomain();
      newAppointment.PersonId = int.Parse(ClaimTypes.NameIdentifier);

      await context.Appointments.AddAsync(newAppointment);

      return Results.Ok();
    }

    [HttpGet]
    public async Task<IResult> GetDoctorAppointment()
    {
      int personId;
      if (HttpContext.User.Identity is ClaimsIdentity identity)
      {
        personId = int.Parse(identity.FindFirst("nameidentifier")!.Value);
      }
      else
      {
        throw new Exception();
      }

      var doctorId = await context.Doctors.Where(d => d.PersonId == personId).Select(d => d.Id).FirstAsync();

      var appointments = context.Appointments.Where(a => a.DoctorId == doctorId);

      return Results.Ok(appointments);
    }

    //TODO: Update status of appointment


    //TODO: Add notes to appointment
  }
}
