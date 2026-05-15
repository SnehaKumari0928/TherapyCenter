using Stripe;
using TherapyCenter.Helpers;
using TherapyCenter2.DTOs.Payment;
using TherapyCenter2.Helper;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Interfaces;


namespace TherapyCenter2.Services.Implementations
{
    public class PaymentService :IPaymentService
    {

        private readonly StripeSettings _settings;
        private readonly IPaymentRepository _paymentRepo;
        private readonly IAppointmentRepository _appointmentRepo;


        public PaymentService(StripeSettings settings, IPaymentRepository paymentRepo, IAppointmentRepository appointmentRepo)
        {
            _settings = settings;
            StripeConfiguration.ApiKey = _settings.SecretKey;
            _paymentRepo = paymentRepo;
            _appointmentRepo = appointmentRepo;
        }

        public async Task<object> CreateOrderAsync(CreateOrderDto dto, int patientId)
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = (long)(dto.Amount * 100),
                Currency = "inr",
                Metadata = new Dictionary<string, string>
                {
                    { "AppointmentId", dto.AppointmentId.ToString() }
                }
            };

            var service = new PaymentIntentService();
            PaymentIntent intent = await service.CreateAsync(options);

            var payment = new Payment
            {
                AppointmentId = dto.AppointmentId,
                Amount = dto.Amount,
                StripePaymentIntentId = intent.Id,
                Status = "pending"

            };

            await _paymentRepo.AddAsync(payment);

            return new
            {
                clientSecret = intent.ClientSecret,
                paymentIntentId = intent.Id
            };
        }

        public async Task<bool> ConfirmPaymentAsync(string paymentIntentId)
        {
            var service = new PaymentIntentService();
            var intent = await service.GetAsync(paymentIntentId);

            if (intent.Status != "succeeded")
                return false;

            var payment = await _paymentRepo.GetByStripeIdAsync(paymentIntentId);

            if (intent.Status != "succeeded") return false;

            payment.Status = "Paid";
            payment.TransactionId = intent.Id;
            payment.PaidAt = DateTime.Now;

            await _paymentRepo.UpdateAsync(payment);

            var appointment = await _appointmentRepo.GetByIdAsync(payment.AppointmentId);

            if(appointment != null)
            {
                appointment.Status = "Confirmed";
                await _appointmentRepo.UpdateAsync(appointment);
            }
            return true;
        }

        public async Task<List<Payment>> GetByPatientIdAsync(int patientId)
        {
            return await _paymentRepo.GetByPatientIdAsync(patientId);
        }
    }
    }

