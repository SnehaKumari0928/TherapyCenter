using TherapyCenter.DTOs.UserDTOs;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Services.Implementation
{
    public class UserService: IUserService
    {

        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _repo.GetAllAsync();

            return users.Select(u => new UserDto
            {
                Id = u.UserId,
                Name = $"{u.FirstName} {u.LastName}",
                Email = u.Email,
                Role = u.Role,
            }).ToList();
        }
        public async Task AddUser(CreateUserDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Role = dto.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)

            };

            await _repo.AddAsync(user);
        }
        public async Task UpdateUser(int id, UpdateUserDto dto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException("User not found");
        
             user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.PhoneNumber = dto.PhoneNumber;

            await _repo.UpdateAsync(user);
        }
        public async Task DeleteUser(int id)
        {
            var user = _repo.GetByIdAsync(id);

            if (user == null)
                throw new KeyNotFoundException("User not found");

            await _repo.DeleteAsync(id);
        }
    }
}
