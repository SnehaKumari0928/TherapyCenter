using TherapyCenter.DTOs.SlotDTOs;

namespace TherapyCenter.Services.Intefaces
{
    public interface ISlotService
    {
        Task<List<SlotDto>> GetAllAvailableSlots(int doctorId, DateTime date);
        Task CreateSlots(CreateSlotDto dto);
    }
}
