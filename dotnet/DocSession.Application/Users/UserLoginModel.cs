namespace DocSession.Entities.Requests;

public class UserLoginModel
{
  public string Username { get; set; } = null!;

  public string Password { get; set; } = null!;
}
