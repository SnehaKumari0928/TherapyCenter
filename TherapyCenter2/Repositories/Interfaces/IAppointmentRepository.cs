using TherapyCenter2.Models;

namespace TherapyCenter2.Repositories.Interfaces
{
    public interface IAppointmentRepository
    {

        Task<Appointment> AddAsync(Appointment appointment);
        Task<List<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int id);
        Task UpdateAsync(Appointment appointment);
        Task DeleteAsync(Appointment appointment);
    }
}
