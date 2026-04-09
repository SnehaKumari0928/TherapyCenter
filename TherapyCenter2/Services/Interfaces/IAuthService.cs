using TherapyCenter2.DTOs.Auth;

namespace TherapyCenter2.Services.Interfaces
{
    public interface IAuthService
    {

        Task<AuthResponseDto> RegisterAsync(RegisterDto request);
        Task<AuthResponseDto?> LoginAsync(LoginDto request);
    }
}
