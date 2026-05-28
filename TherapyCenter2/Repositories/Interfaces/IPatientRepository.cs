using TherapyCenter2.Models;

namespace TherapyCenter2.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> AddAsync(Patient patient);
        Task<Patient?> GetByIdAsync(int id);
        Task<List<Patient>> GetAllAsync();
        Task<Patient> GetByUserIdAsync(int userId);

        Task<Patient?> FindByNameAsync(string firstName, string lastName);

        Task<List<Patient>> GetByGuardianIdAsync(int guardianId);
    }
}
