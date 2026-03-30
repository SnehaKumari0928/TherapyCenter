using TherapyCenter.Entities;

namespace TherapyCenter.Repositories.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> RegisterAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task<bool> UserExistsAsync(string email);
    }
}
