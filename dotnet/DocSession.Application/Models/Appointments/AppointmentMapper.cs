using DocSession.Entities;
using DocSession.Entities.Requests;

namespace DocSession.Application.Appointments;

public static class AppointmentMapper
{
  public static Appointment MapRequestToDomain(this CreateAppointmentModel appointmentDto)
  {
    var result = new Appointment
    {
      Date = DateTimeOffset.Parse(appointmentDto.Date),
      DoctorId = appointmentDto.DoctorId
    };

    return result;
  }
}
