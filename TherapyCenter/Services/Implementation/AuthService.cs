using TherapyCenter.DTOs.AuthDTOs;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Services.Implementation
{
    public class AuthService: IAuthService
    {
        private readonly IAuthRepository _repo;
        private readonly IJwtService _jwt;

        public AuthService(IAuthRepository repo, IJwtService jwt)
        {
            _repo = repo;
            _jwt = jwt;

        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            if(await _repo.UserExistsAsync(dto.Email))
            {
                throw new ArgumentException("User already exists");
            }

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Role = dto.Role,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)


            };

            await _repo.RegisterAsync(user);
            return _jwt.GenerateToken(user);
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _repo.GetByEmailAsync(dto.Email);
            if (user == null)
            {
                throw new KeyNotFoundException("User not found");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                throw new KeyNotFoundException("Invalid credentials");
            return _jwt.GenerateToken(user);
        }
    }
}
