using DocSession.Entities;

namespace DocSession.Application.Models.Statutes;

public class Status
{
  public int Id { get; set; }
  public string Name { get; set; } = nameof(Statutes.Waiting);

  public ICollection<Appointment> Appointments { get; set; }
}

public enum Statutes
{
  Waiting,
  Approved,
  Rejected,
  Passed,
  NotCame
}
