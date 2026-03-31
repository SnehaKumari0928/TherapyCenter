using TherapyCenter.DTOs.TherapyDTOs;

namespace TherapyCenter.Services.Intefaces
{
    public interface ITherapyService
    {
        Task<List<TherapyDto>> GetAllTherapies();
        Task AddTherapy(CreateTherapyDto dto);
        Task UpdateTherapy(int id,UpdateTherapyDto dto);
        Task DeleteTherapy(int id);
    }
}
