using TherapyCenter2.Models;

namespace TherapyCenter2.Repositories.Interfaces
{
    public interface IPaymentRepository
    {

        Task AddAsync(Payment payment);
        Task UpdateAsync(Payment payment);

        Task<Payment?> GetByIdAsync(int id);
        Task<Payment> GetByStripeIdAsync(string stripeId);
        Task<List<Payment>> GetByPatientIdAsync(int patientId);

        Task<List<Payment>> GetAllAsync();
    }
}
