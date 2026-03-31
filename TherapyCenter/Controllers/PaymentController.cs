using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyCenter.DTOs.PaymentDTOs;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _service;

        public PaymentController(IPaymentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Pay(CreatePaymentDto dto)
        {
            await _service.CreatePayment(dto);
            return Ok("Payment successfully");
        }
    }
}
