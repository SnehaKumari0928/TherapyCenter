using TherapyCenter.DTOs.SlotDTOs;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;
using TherapyCenter.Services.Intefaces;

namespace TherapyCenter.Services.Implementation
{
    public class SlotService: ISlotService
    {

        private readonly ISlotRepository _repo;

        public SlotService(ISlotRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<SlotDto>> GetAllAvailableSlots(int doctorId, DateTime date)
        {
            var slots = await _repo.GetAllAvailabelSlotsAsync(doctorId, date);

            return slots.Select(s => new SlotDto
            {

                Id = s.SlotId,
                StartTime = s.StartTime,
                EndTime = s.EndTime
            }).ToList();
        }
        public async Task CreateSlots(CreateSlotDto dto)
        {
            if (dto.StartTime >= dto.EndTime)
                throw new ArgumentException("Invalid time range");

            var slot = new Slot
            {
                DoctorId = dto.DoctorId,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsBooked = false
            };

            await _repo.AddSlotAsync(slot);
        }
    }
}
