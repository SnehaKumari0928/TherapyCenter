using TherapyCenter.DTOs.AuthDTOs;

namespace TherapyCenter.Services.Intefaces
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);

    }
}
