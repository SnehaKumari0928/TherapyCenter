using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyCenter.DTOs.DoctorFindingsDTOs;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Controllers
{
    [Route("api/findings")]
    [ApiController]
    [Authorize(Roles ="Doctor")]
    public class DoctorFindingController : ControllerBase
    {

        private readonly IDoctorFindingService _service;

        public DoctorFindingController(IDoctorFindingService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add(CreateFindingDto dto)
        {
            await _service.AddFinding(dto);
            return Ok("Finding added");
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<IActionResult> GetByAppointment(int appointmentId)
            => Ok(await _service.GetByAppointment(appointmentId));

        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetByPatient(int patientId)
            => Ok(await _service.GetByPatient(patientId));
    }

}

