using DocSession.Application.Models.Persons;
using DocSession.Application.Models.Users.UserCreate;

namespace DocSession.Entities;

public class User
{
  public User()
  {
    
  }

  public User(UserCreateModel model)
  {
    Password = model.Password;
    Person = new Person()
    {
      FirstName = model.FirstName,
      MiddleName = model.MiddleName,
      LastName = model.LastName,
      Email = model.Email,
      PhoneNumber = model.PhoneNumber
    };
  }

  public int Id { get; set; }
  public string Password { get; set; } = null!;

  public Person Person { get; set; }
  public int PersonId { get; set; }
}
