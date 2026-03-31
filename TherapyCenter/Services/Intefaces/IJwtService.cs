using TherapyCenter.Entities;

namespace TherapyCenter.Services.Intefaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);
    }
}
