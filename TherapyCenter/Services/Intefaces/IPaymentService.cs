using TherapyCenter.DTOs.PaymentDTOs;

namespace TherapyCenter.Services.Intefaces
{
    public interface IPaymentService
    {

        Task CreatePayment(CreatePaymentDto dto);
    }
}
