using TherapyCenter2.DTOs.Appointment;

namespace TherapyCenter2.Services.Interfaces
{
    public interface IAppointmentService
    {

        Task<AppointmentResponseDto> CreateAsync(AppointmentCreateDto dto);
        Task<List<AppointmentResponseDto>> GetAllAsync();
        Task<List<AppointmentResponseDto>> GetByPatientIdAsync();
        Task<AppointmentResponseDto> GetByIdAsync(int id);
        Task<AppointmentResponseDto> UpdateAsync(int id, AppointmentUpdateDto dto);
        Task DeleteAsync(int id);
        Task<AppointmentResponseDto> CreateWalkInAsync(WalkInAppointmentDto dto);

        Task CompleteAsync(int id);
        Task CancelAsync(int id);
        Task<List<AppointmentResponseDto>> GetByDoctorIdAsync();

        Task<List<AppointmentResponseDto>> GetAppointmentsForGuardianAsync();
       

       
    }
}
