namespace DocSession.Entities.Requests;

public class CreateAppointmentModel
{
  public string FirstName { get; set; } = null!;

  public string LastName { get; set; } = null!;

  public string PhoneNumber { get; set; } = null!;

  public string? Email { get; set; } = null!;

  public string Date { get; set; } = null!;

  public string Description { get; set; } = null!;

  public int DoctorId { get; set; }
}
