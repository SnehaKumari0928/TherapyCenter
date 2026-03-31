using TherapyCenter.DTOs.AppointmentDTOs;

namespace TherapyCenter.Services.Intefaces
{
    public interface IAppointmentService
    {
        Task BookAppointment(BookAppointmentDto dto);
        Task CancelAppointment(int id);
    }
}
