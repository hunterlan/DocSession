namespace DocSession.Application.Models.Users.UserCreate;

public record UserCreateModel(string FirstName, string? MiddleName, string LastName, string PhoneNumber, string Email, string Password);
