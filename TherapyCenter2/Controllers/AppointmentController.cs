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
      
        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
           
        }


        [Authorize(Roles = "Patient,Guardian,Receptionist")]
        [HttpPost("createappointment")]
        public async Task<IActionResult> Create([FromBody] AppointmentCreateDto dto)
        {
           
            var result = await _appointmentService.CreateAsync(dto);
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
           

            var result = await _appointmentService.GetByPatientIdAsync();

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
            

            var result = await _appointmentService.GetByDoctorIdAsync();
            Console.WriteLine();

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
