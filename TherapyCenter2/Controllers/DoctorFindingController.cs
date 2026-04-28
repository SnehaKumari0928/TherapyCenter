using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TherapyCenter2.DTOs.DoctorFinding;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorFindingController : ControllerBase
    {

        private readonly IDoctorFindingService _service;

        public DoctorFindingController(IDoctorFindingService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Doctor")]
        [HttpPost("create_finding")]
        public async Task<IActionResult> Create(CreateDoctorFindingDto dto)
        {
            var result = await _service.CreateAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }


        [Authorize(Roles = "Patient,Guardian")]
        [HttpGet("my-report")]
        public async Task<IActionResult> GetMyReports()
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            if(userId == null)
            {
                return Unauthorized("Invalid token");
            }

            var result = await _service.GetByPatientIdAsync(userId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetByAppointment(int appointmentId)
        {
            var result = await _service.GetByAppointmentAsync(appointmentId);
            return Ok(result);
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDoctorFindingDto dto)
        {
            var result = await _service.UpdateAsync(id, dto);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return Ok(new { message = "Deleted successfully" });
        }
    }
}
