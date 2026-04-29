using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;

namespace TherapyCenter2.Data
{
    public class DbSeeder
    {

        public static async Task SeedAdminAsync(IServiceProvider services)
        {
            var userRepo = services.GetRequiredService<IUserRepository>();

            var existingAdmin = await userRepo.GetByEmailAsync("harsh@therapy.com");

            if (existingAdmin == null)
            {
                var admin = new User
                {
                    FirstName = "Harsh",
                    LastName = "Singh",
                    Email = "harsh@therapy.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("sneha@12345"),
                    Role = "Admin",
                    PhoneNumber = "8967453289"
                };

                await userRepo.AddUserAsync(admin);
            }

        }
    }
}
