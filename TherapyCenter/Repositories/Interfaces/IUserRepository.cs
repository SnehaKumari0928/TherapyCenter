using TherapyCenter.Entities;

namespace TherapyCenter.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
        Task<List<User>> GetAllAsync();
        Task<List<User>> GetByRoleAsync(string role);

        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);   
    }
}
