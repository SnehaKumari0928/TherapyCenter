using TherapyCenter.DTOs.UserDTOs;

namespace TherapyCenter.Services.Intefaces
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        Task AddUser(CreateUserDto dto);
        Task UpdateUser(int id,UpdateUserDto dto);
        Task DeleteUser(int id);
    }
}
