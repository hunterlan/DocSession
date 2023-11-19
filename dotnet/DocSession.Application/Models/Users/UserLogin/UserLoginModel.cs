namespace DocSession.Application.Models.Users.UserLogin;

public record UserLoginModel
{
  public string Username { get; set; } = null!;

  public string Password { get; set; } = null!;
}
