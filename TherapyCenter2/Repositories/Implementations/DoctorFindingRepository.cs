using TherapyCenter2.Data;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TherapyCenter2.Repositories.Implementations
{
    public class DoctorFindingRepository: IDoctorFindingRepository
    {

        private readonly AppDbContext _context;

        public DoctorFindingRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DoctorFinding> AddAsync(DoctorFinding finding)
        {
            _context.DoctorFindings.Add(finding);
            await _context.SaveChangesAsync();
            return finding;
        }

        public async Task<List<DoctorFinding>> GetAllAsync()
        {
            return await _context.DoctorFindings.ToListAsync();
        }

        
        public async Task<DoctorFinding?> GetByIdAsync(int id)
        {
            return await _context.DoctorFindings.FindAsync(id);
        }

        public async Task<List<DoctorFinding>> GetByPatientIdAsync(int patientId)
        {
            return await _context.DoctorFindings
                .Include(f => f.Appointment)
                .Where(f =>
                f.Appointment.PatientId == patientId)
                .ToListAsync();
        }


        public async Task<List<DoctorFinding>> GetByAppointmentIdAsync(int appointmentId)
        {
            return await _context.DoctorFindings
                .Where(f => f.AppointmentId == appointmentId)
                .ToListAsync();
        }

        public async Task UpdateAsync(DoctorFinding finding)
        {
            _context.DoctorFindings.Update(finding);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(DoctorFinding finding)
        {
            _context.DoctorFindings.Remove(finding);
            await _context.SaveChangesAsync();
        }
    }
}
