using TherapyCenter.Entities;

namespace TherapyCenter.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {

        Task<Appointment> GetByIdAsync(int id);
        Task<List<Appointment>> GetByPatientId(int patientId);
        Task<List<Appointment>> GetByDoctorId(int doctorId);

        Task AddAsync(Appointment appointment);
        Task UpdateAsync(Appointment appointment);
    }
}
