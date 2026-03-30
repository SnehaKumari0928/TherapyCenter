using TherapyCenter.Entities;

namespace TherapyCenter.Repositories.Interfaces
{
    public interface ISlotRepository
    {
        Task<List<Slot>> GetAllAvailabelSlotsAsync();
        Task<Slot> GetByIdAsync(int id);

        Task AddSlotAsync(Slot slot);
        Task UpdateSlotAsync(Slot slot);
        Task DeleteSlotAsync(int id);
    }
}
