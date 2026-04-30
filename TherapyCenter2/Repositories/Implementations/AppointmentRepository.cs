using Microsoft.EntityFrameworkCore;
using TherapyCenter2.Data;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;

namespace TherapyCenter2.Repositories.Implementations
{
    public class AppointmentRepository: IAppointmentRepository
    {

        private readonly AppDbContext _context;

        public AppointmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Appointment> AddAsync(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }
        public async Task<List<Appointment>> GetByPatientIdAsync(int patientid)
        {
            return await _context.Appointments
     .Where(a => a.PatientId == patientid)
     .AsNoTracking()
     .ToListAsync();
        }
        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments.FindAsync(id);
        }

        public async Task UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Appointment>> GetByDoctorIdAsync(int doctorId)
        {
            return await _context.Appointments
                .Where(a => a.DoctorId == doctorId)
                .ToListAsync();
        }

    }
}
