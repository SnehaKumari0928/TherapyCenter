using TherapyCenter.DTOs.DoctorDTOs;

namespace TherapyCenter.Services.Intefaces
{
    public interface IDoctorService
    {
        Task<List<DoctorDto>> GetAllDoctors();
        Task<DoctorDto> GetDoctorById(int id);
        Task AddDoctor(CreateDoctorDto dto);
    }
}
