using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyCenter.DTOs.AuthDTOs;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async  Task<IActionResult> Register(RegisterDto registerDto)
        {
            var token = _authService.RegisterAsync(registerDto);

            return Ok(new { token });
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            var token = _authService.LoginAsync(loginDto);
            return Ok(new { token });
        }
    }
}
