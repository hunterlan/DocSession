namespace DocSession.Entities;

public class Appointment
{
  public int Id { get; set; }

  public string FirstName { get; set; } = null!;

  public string LastName { get; set; } = null!;

  public string PhoneNumber { get; set; } = null!;

  public string? Email { get; set; }

  public DateTimeOffset Date { get; set; }

  public string? Description { get; set; }

  public string Note { get; set; } = null!;

  public int DoctorId { get; set; }

  public Statutes Status { get; set; } = Statutes.Waiting;
}

public enum Statutes
{
  Waiting,
  Approved,
  Rejected,
  Passed,
  NotCame
}
