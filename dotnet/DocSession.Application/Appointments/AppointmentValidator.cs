using DocSession.Entities;
using DocSession.Entities.Requests;
using FluentValidation;

namespace DocSession.Application.Appointments;

public class AppointmentValidator : AbstractValidator<CreateAppointmentModel>
{
  public AppointmentValidator()
  {
    RuleFor(a => a.FirstName)
      .NotEmpty()
      .WithMessage("First name was not specified.");
    RuleFor(a => a.LastName)
      .NotEmpty()
      .WithMessage("Last name was not specified.");;
    RuleFor(a => a.Email)
      .EmailAddress()
      .WithMessage("Incorrect email address was specified.");
    RuleFor(a => a.DoctorId)
      .GreaterThan(0)
      .WithMessage("Incorrect doctor identification number was specified.");
    RuleFor(a => a.Date)
      .Must(BeAValidDate)
      .WithMessage("Incorrect date format");
    RuleFor(a => a.PhoneNumber)
      .NotEmpty()
      .WithMessage("Phone number was not specified.");
  }

  private bool BeAValidDate(string value) => DateTime.TryParse(value, out var date);

}
