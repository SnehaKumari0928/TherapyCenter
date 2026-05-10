using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TherapyCenter2.Data;
using TherapyCenter2.DTOs.Appointment;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {

        private readonly IAppointmentService _appointmentService;
        private readonly AppDbContext _context;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IPatientRepository _patientRepository;
        public AppointmentController(IAppointmentService appointmentService, AppDbContext context, IDoctorRepository doctorRepository, IPatientRepository patientRepository)
        {
            _appointmentService = appointmentService;
            _context = context;
            _doctorRepository = doctorRepository;
            _patientRepository = patientRepository;
        }


        [Authorize(Roles = "Patient,Guardian,Receptionist")]
        [HttpPost("createappointment")]
        public async Task<IActionResult> Create([FromBody] AppointmentCreateDto dto)
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier).Value
            );

            var patient = await _context.Patients.FirstOrDefaultAsync(p => p.GuardianId == userId);

           var  PatientId = patient.PatientId; 
            var result = await _appointmentService.CreateAsync(dto, PatientId);
            return Ok(result);
        }


        [Authorize(Roles = "Admin, Receptionist")]
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
                .FirstOrDefaultAsync(p => p.GuardianId == userId);

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

        [Authorize(Roles = "Receptionist,Admin,Patient")]
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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized("Invalid token");

            int userId = int.Parse(userIdClaim.Value);

            var doctor = await _context.Doctors
                .FirstOrDefaultAsync(d => d.UserId == userId);

            if (doctor == null)
                return BadRequest("Doctor not found");

            var result = await _appointmentService.GetByDoctorIdAsync(doctor.DoctorId);
            Console.WriteLine(doctor.DoctorId);

            return Ok(result);
        }



        [Authorize(Roles = "Receptionist")]
        [HttpPost("walkin")]
        public async Task<IActionResult> CreateWalkIn([FromBody] WalkInAppointmentDto dto)
        {
            try
            {
                var result = await _appointmentService.CreateWalkInAsync(dto);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


      



    }
}
