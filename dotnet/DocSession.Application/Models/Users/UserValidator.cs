using System.Text.RegularExpressions;
using DocSession.Entities.Requests;
using FluentValidation;

namespace DocSession.Application.Models.Users;

public class UserValidator : AbstractValidator<UserLoginModel>
{
  public UserValidator()
  {
    RuleFor(u => u.Username)
      .NotEmpty()
      .WithMessage("Username should be specified.");

    RuleFor(u => u.Password)
      .NotEmpty()
      .WithMessage("Password should be specified.");
  }
}
