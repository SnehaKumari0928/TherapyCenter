using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TherapyCenter2.Data;
using TherapyCenter2.DTOs.Appointment;
using TherapyCenter2.Models;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {

        private readonly IAppointmentService _appointmentService;
        private readonly AppDbContext _context;
        public AppointmentController(IAppointmentService appointmentService, AppDbContext context)
        {
            _appointmentService = appointmentService;
            _context = context;
        }


        [Authorize(Roles = "Patient,Receptionist")]
        [HttpPost("createappointment")]
        public async Task<IActionResult> Create([FromBody] AppointmentCreateDto dto)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier).Value
            );

            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.UserId == userId);

           var  PatientId = patient.PatientId; // CORRECT
            var result = await _appointmentService.CreateAsync(dto, PatientId);
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _appointmentService.GetAllAsync();
            return Ok(result);
        }

        [Authorize(Roles = "Patient")]
        [HttpGet("my")]
        public async Task<IActionResult> GetMyAppointments()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
                return Unauthorized("Invalid token");

            int userId = int.Parse(userIdClaim);

            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (patient == null)
                return BadRequest("Patient not found");

            var result = await _appointmentService.GetByPatientIdAsync(patient.PatientId);

            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _appointmentService.GetByIdAsync(id);
            return Ok(result);
        }


        [Authorize(Roles = "Receptionist,Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AppointmentUpdateDto dto)
        {
            var result = await _appointmentService.UpdateAsync(id, dto);
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _appointmentService.DeleteAsync(id);
            return Ok(new { message = "Appointment deleted successfully" });
        }

        [Authorize(Roles = "Doctor")]
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> Complete(int id)
        {
            await _appointmentService.CompleteAsync(id);
            return Ok(new { message = "Appointment completed" });
        }

        [Authorize(Roles = "Receptionist,Admin")]
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(int id)
        {
            await _appointmentService.CancelAsync(id);
            return Ok(new { message = "Appointment cancelled" });
        }

        [Authorize(Roles = "Doctor")]
        [HttpGet("doctor")]
        public async Task<IActionResult> GetDoctorAppointments()
        {
            var doctorIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if(doctorIdClaim == null)
            {
                return Unauthorized("Invalid token");
            }

            int doctorId = int.Parse(doctorIdClaim.Value);

            var result = await _appointmentService.GetByDoctorIdAsync(doctorId);

            return Ok(result);
        }
    }
}
