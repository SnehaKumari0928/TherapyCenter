using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyCenter.DTOs.TherapyDTOs;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Controllers
{
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class TherapyController : ControllerBase
    {

        private readonly ITherapyService _service;

        public TherapyController(ITherapyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllTherapies());
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTherapyDto dto)
        {
            await _service.AddTherapy(dto);
            return Ok("Therapy added");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTherapyDto dto)
        {
            await _service.UpdateTherapy(id,dto);
            return Ok("Updated");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteTherapy(id);
            return Ok("Deleted");
        }
       
    }
}
