using TherapyCenter.DTOs.DoctorFindingsDTOs;

namespace TherapyCenter.Services.Intefaces
{
    public interface IDoctorFindingService
    {
        Task AddFinding(CreateFindingDto dto);
        Task<List<DoctorFindingDto>> GetByAppointment(int appointmentId);
        Task<List<DoctorFindingDto>> GetByPatient(int patientId);
    }
}
