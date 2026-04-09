using Microsoft.EntityFrameworkCore;
using TherapyCenter2.Data;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
namespace TherapyCenter2.Repositories.Implementations
{
    public class DoctorRepository: IDoctorRepository
    {

        private readonly AppDbContext _context;

        public DoctorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Doctor> AddAsync(Doctor doctor)
        {
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();
            return doctor;
        }

        public async Task<List<Doctor>> GetAllAsync()
        {
            return await _context.Doctors
                .Include(d => d.User)
                .ToListAsync();
        }

        public async Task<Doctor?> GetByIdAsync(int id)
        {
            return await _context.Doctors
                .Include(d => d.User)
                .FirstOrDefaultAsync(d => d.DoctorId == id);
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            _context.Doctors.Update(doctor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Doctor doctor)
        {
            _context.Doctors.Remove(doctor);
            await _context.SaveChangesAsync();
        }
    }
}
