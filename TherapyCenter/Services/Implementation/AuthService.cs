using TherapyCenter.DTOs.AuthDTOs;
using TherapyCenter.Repositories.Interfaces;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Services.Implementation
{
    public class AuthService
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
                throw new Exception("User already exists")
            }
        }

    }
}
