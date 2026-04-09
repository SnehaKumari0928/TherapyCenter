using TherapyCenter2.DTOs.Slot;

namespace TherapyCenter2.Services.Interfaces
{
    public interface ISlotService
    {
        Task<SlotResponseDto> CreateSlotAsync(CreateSlotDto dto);
        Task<List<SlotResponseDto>> GetAllSlotsAsync();
        Task<List<SlotResponseDto>> GetSlotsByDoctorAsync(int doctorId, DateOnly date);
        Task<SlotResponseDto> GetSlotByIdAsync(int id);
        Task<SlotResponseDto> UpdateSlotAsync(int id, UpdateSlotDto dto);
        Task DeleteSlotAsync(int id);
    }
}
