using DocSession.Application.Models.Persons;
using DocSession.Application.Models.Statutes;

namespace DocSession.Entities;

public class Appointment
{
  public int Id { get; set; }

  public DateTimeOffset Date { get; set; }

  public string? Note { get; set; }

  public int DoctorId { get; set; }
  public Doctor Doctor { get; set; }

  public int PersonId { get; set; }
  public Person Person { get; set; }

  public int StatusId { get; set; }
  public Status Status { get; set; }
}
