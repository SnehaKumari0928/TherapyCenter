using Microsoft.EntityFrameworkCore;
using TherapyCenter2.Data;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;

namespace TherapyCenter2.Repositories.Implementations
{
    public class PaymentRepository: IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p =>
                p.Appointment
                )
                .FirstOrDefaultAsync(p => p.PaymentId == id);
        }

        public async Task<Payment?> GetByStripeIdAsync(string stripeId)
        {
            return await _context.Payments
                .FirstOrDefaultAsync(p => p.StripePaymentIntentId == stripeId);
        }

        public async Task<List<Payment>> GetByPatientIdAsync(int patientId)
        {
            return await _context.Payments
                .Include(p => p.Appointment)
                .Where(p =>
                p.Appointment.PatientId == patientId
                ).ToListAsync();
        }

        public async Task<List<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.Appointment)
                .ToListAsync();
        }
    }
}
