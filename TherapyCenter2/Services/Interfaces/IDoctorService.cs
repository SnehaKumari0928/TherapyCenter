using TherapyCenter2.DTOs.Doctor;

namespace TherapyCenter2.Services.Interfaces
{
    public interface IDoctorService
    {

        Task<DoctorResponseDto> CreateDoctorAsync(CreateDoctorDto dto);
        Task<List<DoctorResponseDto>> GetAllDoctorsAsync();
        Task<DoctorResponseDto> GetDoctorByIdAsync(int id);
        Task<DoctorResponseDto> UpdateDoctorAsync(int id, UpdateDoctorDto dto);
        Task DeleteDoctorAsync(int id);
    }
}
