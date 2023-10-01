using DocSession.Entities;
using DocSession.Entities.Requests;

namespace DocSession.Application.Appointments;

public static class AppointmentMapper
{
  public static Appointment MapRequestToDomain(this CreateAppointmentModel appointmentDto)
  {
    var result = new Appointment
    {
      FirstName = appointmentDto.FirstName,
      LastName = appointmentDto.LastName,
      PhoneNumber = appointmentDto.PhoneNumber,
      Email = appointmentDto.Email,
      Date = DateTimeOffset.Parse(appointmentDto.Date),
      Description = appointmentDto.Description,
      DoctorId = appointmentDto.DoctorId
    };

    return result;
  }
}
