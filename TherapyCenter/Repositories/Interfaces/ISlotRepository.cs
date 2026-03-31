using TherapyCenter.Entities;

namespace TherapyCenter.Repositories.Interfaces
{
    public interface ISlotRepository
    {
        Task<List<Slot>> GetAllAvailabelSlotsAsync(int doctorId, DateTime date);
        Task<Slot> GetByIdAsync(int id);

        Task AddSlotAsync(Slot slot);
        Task UpdateSlotAsync(Slot slot);
        
    }
}
