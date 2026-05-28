using Microsoft.AspNetCore.Authorization;
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

        public SlotController(
            ISlotService slotService)
        {
            _slotService = slotService;
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost]
        public async Task<IActionResult>
            Create(CreateSlotDto dto)
        {
            var result =
                await _slotService
                .CreateSlotAsync(dto);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult>
            GetAll()
        {
            var result =
                await _slotService
                .GetAllSlotsAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>
            GetById(int id)
        {
            var result =
                await _slotService
                .GetSlotByIdAsync(id);

            return Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("doctor")]
        public async Task<IActionResult>
            GetByDoctor(
            [FromQuery]
            DateOnly date)
        {
            var result =
                await _slotService
                .GetSlotsByDoctorAsync(
                    date);

            return Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("{id}")]
        public async Task<IActionResult>
            Update(
            int id,
            UpdateSlotDto dto)
        {
            var result =
                await _slotService
                .UpdateSlotAsync(
                    id,
                    dto);

            return Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpDelete("{id}")]
        public async Task<IActionResult>
            Delete(int id)
        {
            await _slotService
                .DeleteSlotAsync(id);

            return Ok(new
            {
                message =
                "Slot deleted successfully"
            });
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet(
            "doctor/generated")]
        public async Task<IActionResult>
            GetGenerated(
            [FromQuery]
            DateOnly date)
        {
            var result =
                await _slotService
                .GetGeneratedSlotsByDoctorAsync(
                    date);

            return Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost("bulk")]
        public async Task<IActionResult>
            CreateBulk(
            [FromBody]
            CreateBulkSlotDto dto)
        {
            await _slotService
                .CreateBulkSlotsAsync(dto);

            return Ok(new
            {
                message =
                    "Slots generated successfully"
            });
        }
    }
}