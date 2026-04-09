using TherapyCenter2.Data;
using TherapyCenter2.Models;
using Microsoft.EntityFrameworkCore;
using TherapyCenter2.Repositories.Interfaces;

namespace TherapyCenter2.Repositories.Implementations
{
    public class UserRepository :  IUserRepository
    {

        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FindAsync(email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
