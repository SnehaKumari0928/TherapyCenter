using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyCenter2.DTOs.Doctor;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {

        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDoctorDto dto)
        {
            var result = await _doctorService.CreateDoctorAsync(dto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _doctorService.GetAllDoctorsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _doctorService.GetDoctorByIdAsync(id);
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateDoctorDto dto)
        {
            var result = await _doctorService.UpdateDoctorAsync(id, dto);
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _doctorService.DeleteDoctorAsync(id);
            return Ok(new { message = "Doctor deleted successfully" });
        }
    }
}
