using DocSession.Entities;

namespace DocSession.Application.Models.Persons;

public class Doctor
{
  public int Id { get; set; }

  public int PersonId { get; set; }
  public Person Person { get; set; }

  public ICollection<Appointment> Appointments { get; set; }
}
