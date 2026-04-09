using TherapyCenter2.DTOs.Therapy;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Services.Implementations
{
    public class TherapyService: ITherapyService
    {

        private readonly ITherapyRepository _therapyRepository;

        public TherapyService(ITherapyRepository therapyRepository)
        {
            _therapyRepository = therapyRepository;
        }

        public async Task<TherapyResponseDto> CreateTherapyAsync(CreateTherapyDto dto)
        {
            var therapy = new Therapy
            {
                Name = dto.Name,
                Description = dto.Description,
                DurationMinutes = dto.DurationMinutes,
                Cost = dto.Cost
            };

            var created = await _therapyRepository.AddAsync(therapy);

            return TherapyMapToResponse(created);
        }

        public async Task<List<TherapyResponseDto>> GetAllTherapiesAsync()
        {
            var therapies = await _therapyRepository.GetAllAsync();

            return therapies.Select(TherapyMapToResponse).ToList();
        }

        public async Task<TherapyResponseDto> GetTherapyByIdAsync(int id)
        {
            var therapy = await _therapyRepository.GetByIdAsync(id);

            if (therapy == null)
                throw new Exception("Therapy not found");

            return TherapyMapToResponse(therapy);
        }

        public async Task<TherapyResponseDto> UpdateTherapyAsync(int id, UpdateTherapyDto dto)
        {
            var therapy = await _therapyRepository.GetByIdAsync(id);

            if (therapy == null)
                throw new Exception("Therapy not found");

            therapy.Name = dto.Name;
            therapy.Description = dto.Description;
            therapy.DurationMinutes = dto.DurationMinutes;
            therapy.Cost = dto.Cost;

            await _therapyRepository.UpdateAsync(therapy);

            return TherapyMapToResponse(therapy);
        }

        public async Task DeleteTherapyAsync(int id)
        {
            var therapy = await _therapyRepository.GetByIdAsync(id);

            if (therapy == null)
                throw new Exception("Therapy not found");

            await _therapyRepository.DeleteAsync(therapy);
        }

        private static TherapyResponseDto TherapyMapToResponse(Therapy therapy)
        {
            return new TherapyResponseDto
            {
                TherapyId = therapy.TherapyId,
                Name = therapy.Name,
                Description = therapy.Description,
                DurationMinutes = therapy.DurationMinutes,
                Cost = therapy.Cost
            };
        }
    }
}
