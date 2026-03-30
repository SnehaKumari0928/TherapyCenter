using TherapyCenter.Entities;

namespace TherapyCenter.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {

        Task<Appointment> GetByIdAsync(int id);
        Task<Appointment> GetByPatientId(int patientId);
        Task<Appointment> GetByDoctorId(int doctorId);

        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
    }
}
