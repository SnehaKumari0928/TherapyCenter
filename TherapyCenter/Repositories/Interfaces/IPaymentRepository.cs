using TherapyCenter.Entities;

namespace TherapyCenter.Repositories.Interfaces
{
    public interface IPaymentRepository
    {
        Task<Payment> GetByIdAsync(int id);
        Task<List<Payment>> GetByPatientIdAsync(int patientId);
        Task AddAsync(Payment payment);
    }
}
