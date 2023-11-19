using DocSession.Entities;

namespace DocSession.Application.Models.Persons
{
  public class Person
  {
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string? MiddleName { get; set; }

    public string LastName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public Doctor? Doctor { get; set; }
    public Admin? Admin { get; set; }


    public ICollection<Appointment> Appointments { get; set; }
    public ICollection<User> Users { get; set; }
  }
}
