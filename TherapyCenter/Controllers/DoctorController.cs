using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyCenter.DTOs.DoctorDTOs;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _service;

        public DoctorController(IDoctorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllDoctors());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        => Ok(await _service.GetDoctorById(id));

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateDoctorDto dto)
        {
            await _service.AddDoctor(dto);
            return Ok("Doctor added");
        }

       
    
}
}
