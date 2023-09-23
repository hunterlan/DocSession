namespace DocSession.Entities;

public class Appointment
{
  public int Id { get; set; }

  public string FirstName { get; set; } = null!;

  public string LastName { get; set; } = null!;

  public string PhoneNumber { get; set; } = null!;

  public string Email { get; set; } = null!;

  public string Date { get; set; } = null!;

  public string Description { get; set; } = null!;

  public string Note { get; set; } = null!;

  public int DoctorId { get; set; }

  public Statutes Status { get; set; }
}

public enum Statutes
{
  Waiting,
  Approved,
  Rejected,
  Passed,
  NotCame
}
