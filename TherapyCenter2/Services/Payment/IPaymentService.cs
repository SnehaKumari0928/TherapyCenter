using TherapyCenter2.DTOs.Payment;

namespace TherapyCenter2.Services.Payment
{
    public interface IPaymentService
    {
        Task<string> CreateOrderAsync(CreateOrderDto dto);
    }
}
