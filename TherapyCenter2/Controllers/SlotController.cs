using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyCenter2.DTOs.Slot;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {

        private readonly ISlotService _slotService;

        public SlotController(ISlotService slotService)
        {
            _slotService = slotService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateSlotDto dto)
        {
            var result = await _slotService.CreateSlotAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _slotService.GetAllSlotsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _slotService.GetSlotByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor(int doctorId, [FromQuery] DateOnly date)
        {
            var result = await _slotService.GetSlotsByDoctorAsync(doctorId, date);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSlotDto dto)
        {
            var result = await _slotService.UpdateSlotAsync(id, dto);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _slotService.DeleteSlotAsync(id);
            return Ok(new { message = "Slot deleted successfully" });
        }
    }
}
