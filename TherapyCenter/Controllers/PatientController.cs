using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyCenter.DTOs.PatientDTOs;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Receptionist")]
    [ApiController]
    public class PatientController : ControllerBase
    {

        private readonly IPatientService _service;
        public PatientController(IPatientService service)
        {
            _service = service;
        }


        [HttpGet]

        public async Task<IActionResult> GetAll()
        {

            return Ok(await _service.GetAllPatients());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePatientDto dto)
        {
            await _service.AddPatient(dto);
            return Ok("Patient added");
        }
    }
}
