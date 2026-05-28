using Microsoft.EntityFrameworkCore;
using TherapyCenter2.Data;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;

namespace TherapyCenter2.Repositories.Implementations
{
    public class PatientRepository: IPatientRepository
    {


        private readonly AppDbContext _context;

        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Patient> AddAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient?> GetByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }

        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }

        public async Task<Patient?> FindByNameAsync(string firstName, string lastName)
        {
            return await _context.Patients
                .FirstOrDefaultAsync(p => p.FirstName == firstName && p.LastName == lastName);
        }


        public async Task<List<Patient>> GetByGuardianIdAsync(int guardianId)
        {
            return await _context.Patients
                .Where(p => p.UserId == guardianId)
                .ToListAsync();
        }

        public async Task<Patient> GetByUserIdAsync(int userId)
        {
            return await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);
        }


    }
}
