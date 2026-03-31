using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyCenter.DTOs.SlotDTOs;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Controllers
{
    [Route("api/slots")]
    [ApiController]
    [Authorize]
    public class SlotController : ControllerBase
    {

        private readonly ISlotService _service;

        public SlotController(ISlotService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailable(int doctorId, DateTime date)
       => Ok(await _service.GetAllAvailableSlots(doctorId, date));

        [HttpPost]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<IActionResult> Create(CreateSlotDto dto)
        {
            await _service.CreateSlots(dto);
            return Ok("Slot created");
        }

    }
}
