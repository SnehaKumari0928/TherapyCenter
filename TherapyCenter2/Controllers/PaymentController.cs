using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyCenter2.DTOs.Payment;
using TherapyCenter2.Services.Payment;

namespace TherapyCenter2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost("create_order")]
        public IActionResult CreateOrder(CreateOrderDto dto)
        {
            var order = _paymentService.CreateOrderAsync(dto);
            return Ok(order);
        }
    }
}
