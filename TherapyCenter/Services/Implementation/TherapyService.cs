using TherapyCenter.DTOs.TherapyDTOs;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Services.Implementation
{
    public class TherapyService: ITherapyService
    {

        private readonly ITherapyRepository _repo;

        public TherapyService(ITherapyRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<TherapyDto>> GetAllTherapies()
        {
            var therapies = await _repo.GetAllAsync();

            return therapies.Select(t => new TherapyDto
            {
                Id = t.TherapyId,
                Name = t.Name,
                Cost = t.Cost,
            }).ToList();
        }
        public async Task AddTherapy(CreateTherapyDto dto)
        {

            if (dto.Cost <= 0)
                throw new ArgumentException("Invalid therapy cost");
            var therapy = new Therapy
            {
                Name = dto.Name,
                Cost = dto.Cost,
                Description = dto.Description,
                DurationMinutes = dto.Duration

            };

            await _repo.AddAsync(therapy);
        }
        public async Task UpdateTherapy(int id,UpdateTherapyDto dto)
        {
            var therapy = await _repo.GetByIdAsync(id);

            if(therapy == null)
                throw new KeyNotFoundException("Therapy not found");

            therapy.Name = dto.Name;
            therapy.Description = dto.Description;
            therapy.DurationMinutes = dto.Duration;
            therapy.Cost = dto.Cost;

            await _repo.UpdateAsync(therapy);
        }
        public async Task DeleteTherapy(int id)
        {
            var therapy = await _repo.GetByIdAsync(id);

            if (therapy == null)
                throw new KeyNotFoundException("Therapy not found");

            await _repo.DeleteAsync(id);
        }
    }
}
