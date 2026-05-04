using TherapyCenter2.DTOs.Payment;
using TherapyCenter2.Models;

namespace TherapyCenter2.Services.Interfaces
{
    public interface IPaymentService
    {
        Task<object> CreateOrderAsync(CreateOrderDto dto, int patientId);

        Task<bool> ConfirmPaymentAsync(string paymentIntentId);
        Task<List<Payment>> GetByPatientIdAsync(int patientId);
    }
}
