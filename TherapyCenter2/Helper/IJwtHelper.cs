using TherapyCenter2.Models;

namespace TherapyCenter2.Helper
{
    public interface IJwtHelper
    {
        string GenerateToken(User user);
    }
}
