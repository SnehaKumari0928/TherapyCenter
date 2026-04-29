using TherapyCenter2.DTOs.User;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TherapyCenter2.Services.Implementations
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            var users = await _repo.GetAllAsync();

            if (users == null)
                return new List<UserResponseDto>();

            return users.Select(u => new UserResponseDto
            {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = u.Role,
                PhoneNumber = u.PhoneNumber,
                CreatedAt = u.CreatedAt,
                IsActive = u.IsActive
            }).ToList();
        }

        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            return new UserResponseDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Role = user.Role,
                PhoneNumber = user.PhoneNumber,
                CreatedAt = user.CreatedAt,
                IsActive = user.IsActive
            };
        }

        public async Task<UserResponseDto> CreateAsync(UserCreateDto dto)
        {
            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = dto.Role,
                PhoneNumber = dto.PhoneNumber,
                CreatedAt = DateTime.Now,
                IsActive = true
            };

            var created = await _repo.AddUserAsync(user);

            return new UserResponseDto
            {
                UserId = created.UserId,
                FirstName = created.FirstName,
                LastName = created.LastName,
                Email = created.Email,
                Role = created.Role,
                PhoneNumber = created.PhoneNumber,
                CreatedAt = created.CreatedAt,
                IsActive = created.IsActive
            };
        }

        public async Task<UserResponseDto> UpdateAsync(int id, UserUpdateDto dto)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            if (dto.FirstName != null) user.FirstName = dto.FirstName;
            if (dto.LastName != null) user.LastName = dto.LastName;
            if (dto.PhoneNo != null) user.PhoneNumber = dto.PhoneNo;
            if (dto.IsActive != null) user.IsActive = dto.IsActive.Value;

            var updated = await _repo.UpdateUserAsync(user);

            return new UserResponseDto
            {
                UserId = updated.UserId,
                FirstName = updated.FirstName,
                LastName = updated.LastName,
                Email = updated.Email,
                Role = updated.Role,
                PhoneNumber = updated.PhoneNumber,
                CreatedAt = updated.CreatedAt,
                IsActive = updated.IsActive
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _repo.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User with ID {id} not found");

            return await _repo.DeleteAsync(user);
        }
    }
}
