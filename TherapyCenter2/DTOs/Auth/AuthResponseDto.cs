namespace TherapyCenter2.DTOs.Auth
{
    public class AuthResponseDto
    {
        public int UserId { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public string FirstName { get; set; }

        public string Token { get; set; } = string.Empty;
    }
}
