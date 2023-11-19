using DocSession.Entities;

namespace DocSession.Application.Models.Persons;

public class Admin
{
  public int Id { get; set; }

  public int PersonId { get; set; }
  public Person Person { get; set; }
}
