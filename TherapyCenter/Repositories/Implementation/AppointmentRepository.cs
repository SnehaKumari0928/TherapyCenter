using Microsoft.EntityFrameworkCore;
using TherapyCenter.Data;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;

namespace TherapyCenter.Repositories.Implementation
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> GetByIdAsync(int id)
            => await _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Doctor)
                .Include(a => a.Therapy)
                .FirstOrDefaultAsync(a => a.Appointmentid == id);

        public async Task<List<Appointment>> GetByPatientId(int patientId)
            => await _context.Appointments
                .Where(x => x.PatientId == patientId)
                .ToListAsync();

        public async Task<List<Appointment>> GetByDoctorId(int doctorId)
            => await _context.Appointments
                .Where(x => x.DoctorId == doctorId)
                .ToListAsync();

        public async Task AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }
    }
}
