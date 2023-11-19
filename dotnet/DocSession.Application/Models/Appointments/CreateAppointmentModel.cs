namespace DocSession.Entities.Requests;

public record CreateAppointmentModel
{
  public string FirstName { get; set; } = null!;

  public string LastName { get; set; } = null!;

  public string PhoneNumber { get; set; } = null!;

  public string? Email { get; set; } = null!;

  public string Date { get; set; } = null!;

  public string? Description { get; set; }

  public int DoctorId { get; set; }
}
