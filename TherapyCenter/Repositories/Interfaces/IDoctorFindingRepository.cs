using TherapyCenter.Entities;

namespace TherapyCenter.Repositories.Interfaces
{
    public interface IDoctorFindingRepository
    {

        Task<DoctorFinding> GetByPatientIdAsync(int PatientId);
        Task<DoctorFinding> GetByAppointmentId(int appointmentId);

        Task AddAsync(DoctorFinding doctorFinding);

    }
}
