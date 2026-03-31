using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyCenter.DTOs.AppointmentDTOs;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Controllers
{
    [Route("api/appointments")]
    [ApiController]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Patient,Receptionist")]
        public async Task<IActionResult> Book(BookAppointmentDto dto)
        {
            await _service.BookAppointment(dto);
            return Ok("Appointment booked");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(int id)
        {
            await _service.CancelAppointment(id);
            return Ok("Cancelled");
        }
    }
}
