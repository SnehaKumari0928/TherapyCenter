using Microsoft.EntityFrameworkCore;
using TherapyCenter.Data;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;

namespace TherapyCenter.Repositories.Implementation
{
    public class PatientRepository: IPatientRepository
    {

        private readonly AppDbContext _context;
        public PatientRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id);
        }
        public async Task<List<Patient>> GetAllAsync()
        {
            return await _context.Patients.ToListAsync();
        }
        public async Task<List<Patient>> GetByGuardianIdAsync(int guardianId)
        {
          return  await _context.Patients
               .Where(x => x.GuardianId == guardianId).ToListAsync();
        }
        public async Task AddAsync(Patient patient)
        {
             await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateAsync(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
        }
    }
}
