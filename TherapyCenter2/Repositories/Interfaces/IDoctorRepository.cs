using TherapyCenter2.Models;

namespace TherapyCenter2.Repositories.Interfaces
{
    public interface IDoctorRepository
    {

        Task<Doctor> AddAsync(Doctor doctor);
        Task<List<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(int id);
        Task UpdateAsync(Doctor doctor);
        Task DeleteAsync(Doctor doctor);
    }
}
