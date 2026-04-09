using TherapyCenter2.Models;

namespace TherapyCenter2.Repositories.Interfaces
{
    public interface ITherapyRepository
    {

        Task<Therapy> AddAsync(Therapy therapy);
        Task<List<Therapy>> GetAllAsync();
        Task<Therapy?> GetByIdAsync(int id);
        Task UpdateAsync(Therapy therapy);
        Task DeleteAsync(Therapy therapy);
    }
}
