using TherapyCenter.Entities;

namespace TherapyCenter.Repositories.Interfaces
{
    public interface IDoctorFindingRepository
    {

        Task<List<DoctorFinding>> GetByPatientIdAsync(int PatientId);
        Task<List<DoctorFinding>> GetByAppointmentId(int appointmentId);

        Task AddAsync(DoctorFinding doctorFinding);

    }
}
