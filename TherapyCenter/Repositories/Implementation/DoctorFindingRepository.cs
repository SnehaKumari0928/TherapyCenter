using Microsoft.EntityFrameworkCore;
using TherapyCenter.Data;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;

namespace TherapyCenter.Repositories.Implementation
{
    public class DoctorFindingRepository : IDoctorFindingRepository
    {
        private readonly AppDbContext _context;

        public DoctorFindingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<DoctorFinding>> GetByPatientIdAsync(int patientId)
            => await _context.DoctorFindings
                .Include(x => x.Appointment)
                .Where(x => x.Appointment.PatientId == patientId)
                .ToListAsync();

        public async Task<List<DoctorFinding>> GetByAppointmentId(int appointmentId)
            => await _context.DoctorFindings
                .Where(x => x.AppointmentId == appointmentId).ToListAsync();

        public async Task AddAsync(DoctorFinding finding)
        {
            await _context.DoctorFindings.AddAsync(finding);
            await _context.SaveChangesAsync();
        }
    }
}
