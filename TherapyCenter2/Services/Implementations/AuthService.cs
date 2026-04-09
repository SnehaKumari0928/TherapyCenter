using TherapyCenter2.DTOs.Auth;
using TherapyCenter2.Helper;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Services.Implementations
{
    public class AuthService: IAuthService
    {

        private readonly IUserRepository _userRepository;
        private readonly IJwtHelper _jwtHelper;

        public AuthService(IUserRepository userRepository, IJwtHelper jwtHelper)
        {
            _userRepository = userRepository;
            _jwtHelper = jwtHelper;
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
        {
            var existingUser = await _userRepository.GetByEmailAsync(dto.Email);
            if (existingUser != null)
                throw new Exception("User already exists");

            var allowedRoles = new[] { "Receptionist", "Doctor", "Guardian" };

            if (!allowedRoles.Contains(dto.Role))
                throw new Exception("Invalid role selection");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Role = dto.Role,
                PhoneNumber = dto.PhoneNumber
            };

            var createdUser = await _userRepository.AddUserAsync(user);

            return new AuthResponseDto
            {
                UserId = createdUser.UserId,
                Email = createdUser.Email,
                Role = createdUser.Role,
                Token = "" 
            };
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null)
                return null;

            var isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if (!isPasswordValid)
                return null;

            var token = _jwtHelper.GenerateToken(user);

            return new AuthResponseDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Role = user.Role,
                Token = token 
            };
        }
    }
}
