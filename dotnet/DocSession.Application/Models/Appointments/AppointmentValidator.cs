using DocSession.Entities;
using DocSession.Entities.Requests;
using FluentValidation;

namespace DocSession.Application.Appointments;

public class AppointmentValidator : AbstractValidator<CreateAppointmentModel>
{
  public AppointmentValidator()
  {
    RuleFor(a => a.DoctorId)
      .GreaterThan(0)
      .WithMessage("Incorrect doctor identification number was specified.");
    RuleFor(a => a.Date)
      .Must(BeAValidDate)
      .WithMessage("Incorrect date format");
  }

  private bool BeAValidDate(string value) => DateTime.TryParse(value, out var date);

}
