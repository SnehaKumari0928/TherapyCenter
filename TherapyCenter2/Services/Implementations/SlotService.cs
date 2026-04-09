using TherapyCenter2.DTOs.Slot;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using TherapyCenter2.Services.Interfaces;

namespace TherapyCenter2.Services.Implementations
{
    public class SlotService: ISlotService
    {
        private readonly ISlotRepository _slotRepository;

        public SlotService(ISlotRepository slotRepository)
        {
            _slotRepository = slotRepository;
        }

        public async Task<SlotResponseDto> CreateSlotAsync(CreateSlotDto dto)
        {
            if (dto.StartTime >= dto.EndTime)
                throw new Exception("Invalid time");

            var existingSlots = await _slotRepository.GetByDoctorAndDateAsync(dto.DoctorId, dto.Date);

            foreach (var s in existingSlots)
            {
                bool overlapedSlots = dto.StartTime < s.EndTime && dto.EndTime > s.StartTime;

                if (overlapedSlots)
                    throw new Exception("Slots are overlapped");
            }

            var slot = new Slot
            {
                DoctorId = dto.DoctorId,
                Date = dto.Date,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                IsBooked = false
            };

            var created = await _slotRepository.AddAsync(slot);

            return MapSlotResponse(created);
        }

        public async Task<List<SlotResponseDto>> GetAllSlotsAsync()
        {
            var slots = await _slotRepository.GetAllAsync();
            return slots.Select(MapSlotResponse).ToList();
        }

        public async Task<List<SlotResponseDto>> GetSlotsByDoctorAsync(int doctorId, DateOnly date)
        {
            var slots = await _slotRepository.GetByDoctorAndDateAsync(doctorId, date);
            return slots.Select(MapSlotResponse).ToList();
        }

        public async Task<SlotResponseDto> GetSlotByIdAsync(int id)
        {
            var slot = await _slotRepository.GetByIdAsync(id);

            if (slot == null)
                throw new Exception("Slot not found");

            return MapSlotResponse(slot);
        }

        public async Task<SlotResponseDto> UpdateSlotAsync(int id, UpdateSlotDto dto)
        {
            var slot = await _slotRepository.GetByIdAsync(id);

            if (slot == null)
                throw new Exception("Slot not found");

            if (dto.StartTime >= dto.EndTime)
                throw new Exception("Invalid time");

            var existingSlots = await _slotRepository.GetByDoctorAndDateAsync(slot.DoctorId, dto.Date);

            foreach (var s in existingSlots)
            {
                if (s.SlotId == id) continue;

                bool overlap = dto.StartTime < s.EndTime && dto.EndTime > s.StartTime;

                if (overlap)
                    throw new Exception("Slots are overlapped");
            }

            slot.Date = dto.Date;
            slot.StartTime = dto.StartTime;
            slot.EndTime = dto.EndTime;

            await _slotRepository.UpdateAsync(slot);

            return MapSlotResponse(slot);
        }

        public async Task DeleteSlotAsync(int id)
        {
            var slot = await _slotRepository.GetByIdAsync(id);

            if (slot == null)
                throw new Exception("Slot not found");

            await _slotRepository.DeleteAsync(slot);
        }

        private static SlotResponseDto MapSlotResponse(Slot slot)
        {
            return new SlotResponseDto
            {
                SlotId = slot.SlotId,
                DoctorId = slot.DoctorId,
                Date = slot.Date,
                StartTime = slot.StartTime,
                EndTime = slot.EndTime,
                IsBooked = slot.IsBooked
            };
        }
    }
}
