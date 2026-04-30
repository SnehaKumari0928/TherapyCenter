using TherapyCenter2.Models;

namespace TherapyCenter2.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {

        Task<Appointment> AddAsync(Appointment appointment);
        Task<List<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int id);

        Task<List<Appointment>> GetByPatientIdAsync(int patientId);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(Appointment appointment);
        Task<List<Appointment>> GetByDoctorIdAsync(int doctorId);

    }
}
