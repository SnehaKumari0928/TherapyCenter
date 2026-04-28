using TherapyCenter2.DTOs.DoctorFinding;

namespace TherapyCenter2.Services.Interfaces
{
    public interface IDoctorFindingService
    {

        Task<DoctorFindingResponseDto> CreateAsync(CreateDoctorFindingDto dto);
        Task<List<DoctorFindingResponseDto>> GetAllAsync();
        Task<DoctorFindingResponseDto> GetByIdAsync(int id);
        Task<List<DoctorFindingResponseDto>> GetByAppointmentAsync(int appointmentId);
        Task<DoctorFindingResponseDto> UpdateAsync(int id, UpdateDoctorFindingDto dto);
        Task DeleteAsync(int id);

        Task<List<DoctorFindingResponseDto>> GetByPatientIdAsync(int patientId);
    }
}
