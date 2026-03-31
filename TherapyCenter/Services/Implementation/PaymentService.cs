using TherapyCenter.DTOs.PaymentDTOs;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Services.Implementation
{
    public class PaymentService: IPaymentService
    {
        private readonly IPaymentRepository _repo;

        public PaymentService(IPaymentRepository repo)
        {
            _repo = repo;
        }

        public async Task CreatePayment(CreatePaymentDto dto)
        {
            if(dto.Amount <= 0)
            {
                throw new ArgumentException("Invalid payment amount");
            }

            var payment = new Payment
            {
                AppointmentId = dto.AppointmentId,
                Amount = dto.Amount,
                PaymentMethod = dto.Method,
                Status = "Paid",
                PaidAt = DateTime.UtcNow
            };

            await _repo.AddAsync(payment);
        }

    }
}
