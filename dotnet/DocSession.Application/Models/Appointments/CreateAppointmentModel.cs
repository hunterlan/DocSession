namespace DocSession.Entities.Requests;

public record CreateAppointmentModel
{
  public string Date { get; set; } = null!;

  public int DoctorId { get; set; }
}
