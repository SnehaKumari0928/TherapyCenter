using TherapyCenter.Data;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;

namespace TherapyCenter.Repositories.Implementation
{
    public class SlotRepository: ISlotRepository
    {

        private readonly AppDbContext _context;

        public SlotRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Slot>> GetAllAvailabelSlotsAsync()
        {

        }
        public async Task<Slot> GetByIdAsync(int id)
        {

        }

        public async Task AddSlotAsync(Slot slot)
        {

        }
        public async Task UpdateSlotAsync(Slot slot)
        {

        }
        Task DeleteSlotAsync(int id);
    }
}
