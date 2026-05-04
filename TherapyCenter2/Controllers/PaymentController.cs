using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using System.Security.Claims;
using TherapyCenter2.DTOs.Payment;
using TherapyCenter2.Services.Interfaces;

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

        [Authorize (Roles = "Patient,Guardian")]
        [HttpPost("create_order")]
        public async  Task<IActionResult> CreateOrder(CreateOrderDto dto)
        {
            try
            {
                var userId = int.Parse(
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"
                );

                var result = await _paymentService.CreateOrderAsync(dto, userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Patient,Guardian")]
        [HttpPost("confirm")]
        public async Task<IActionResult> Confirm([FromBody] ConfirmPaymentDto dto)
        {
            try
            {
                var success = await _paymentService.ConfirmPaymentAsync(dto.PaymentIntentId);

                if (!success)
                    return BadRequest("Payment not completed");

                return Ok(new { message = "Payment successful" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Patient,Guardian")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyPayments()
        {
            try
            {
                var userId = int.Parse(
                    User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"
                );

                var result = await _paymentService.GetByPatientIdAsync(userId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
