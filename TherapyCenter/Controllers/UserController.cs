using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TherapyCenter.DTOs.UserDTOs;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Controllers
{
    [Route("api/users")]
    [Authorize(Roles = "Admin")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllUsers());

        }
        [HttpPost]

        public async Task<IActionResult> Create(CreateUserDto dto)
        {
            await _service.AddUser(dto);
            return Ok(dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,UpdateUserDto dto)
        {
            await _service.UpdateUser(id, dto);
            return Ok("Updated");
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {

            await _service.DeleteUser(id);
            return Ok("Deleted");
        }
    }
}
