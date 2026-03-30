using Microsoft.OpenApi;
using TherapyCenter.Entities;

namespace TherapyCenter.Repositories.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> GetByIdAsync(int id);
        Task<List<Patient>> GetAllAsync();
        Task<List<Patient>> GetByGuardianIdAsync(int guardianId);
        Task AddAsync(Patient patient);
        Task UpdateAsync(Patient patient);
    }
}
