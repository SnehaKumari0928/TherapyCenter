using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TherapyCenter2.DTOs.Slot;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SlotController : ControllerBase
    {

        private readonly ISlotService _slotService;
        private readonly IDoctorRepository _doctorRepository;

        public SlotController(ISlotService slotService, IDoctorRepository DoctorRepository)
        {
            _slotService = slotService;
            _doctorRepository = DoctorRepository;
        }

        [Authorize(Roles = "Doctor")]
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
        public async Task<IActionResult> GetByDoctor(int doctorId, [FromQuery] string date)
        {
            var parsedDate = DateOnly.Parse(date);

            var result = await _slotService.GetSlotsByDoctorAsync(doctorId, parsedDate);
            return Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSlotDto dto)
        {
            var result = await _slotService.UpdateSlotAsync(id, dto);
            return Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _slotService.DeleteSlotAsync(id);
            return Ok(new { message = "Slot deleted successfully" });
        }

        [HttpGet("doctor/{doctorId}/generated")]
        public async Task<IActionResult> GetGenerated(int doctorId, [FromQuery] DateOnly date)
        {
            var result = await _slotService.GetGeneratedSlotsByDoctorAsync(doctorId, date);
            return Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost("bulk")]
        public async Task<IActionResult> CreateBulk([FromBody]
    CreateBulkSlotDto dto
)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!
                .Value
            );

            var doctor = await _doctorRepository
                .GetByUserIdAsync(userId);

            if (doctor == null)
                return BadRequest("Doctor not found");

            await _slotService.CreateBulkSlotsAsync(
                dto,
                doctor.DoctorId
            );

            return Ok(new
            {
                message = "Slots generated successfully"
            });
        }
    }
}
