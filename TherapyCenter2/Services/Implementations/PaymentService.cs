using Stripe;
using TherapyCenter.Helpers;
using TherapyCenter2.DTOs.Payment;
using TherapyCenter2.Helper;


namespace TherapyCenter2.Services.Payment
{
    public class PaymentService :IPaymentService
    {

        private readonly StripeSettings _settings;

        public PaymentService(StripeSettings settings)
        {
            _settings = settings;
            StripeConfiguration.ApiKey = _settings.SecretKey;
        }

        public async Task<string> CreateOrderAsync(CreateOrderDto dto)
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

            return intent.ClientSecret;
        }
    }
    }

