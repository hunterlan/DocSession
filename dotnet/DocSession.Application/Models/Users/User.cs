namespace DocSession.Entities;

public class User
{
  public int Id { get; set; }

  public string Username { get; set; } = null!;

  public string Password { get; set; } = null!;

  public string Fullname { get; set; } = null!;

  public string Email { get; set; } = null!;

  public bool IsAdmin { get; set; }
}
