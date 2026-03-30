using TherapyCenter.Entities;

namespace TherapyCenter.Repositories.Interfaces
{
    public interface IDoctorRepository
    {

        Task<Doctor> GetByIdAsync(int id);
        Task<List<Doctor>> GetAllAsync();
        Task AddAsync(Doctor doctor);
        Task UpdateAsync(Doctor doctor);
    }
}
