using TherapyCenter.Entities;

namespace TherapyCenter.Repositories.Interfaces
{
    public interface ITherapyRepository
    {
        Task<Therapy> GetByIdAsync(int id);
        Task<List<Therapy>> GetAllAsync();
        Task AddAsync(Therapy therapy);
        Task UpdateAsync(Therapy therapy);
        Task DeleteAsync(int id);
    }
}
