using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Slot>> GetAllAvailabelSlotsAsync(int doctorId, DateTime date)
        {
          return   await _context.Slots
            .Where(x => x.DoctorId == doctorId &&
                        x.Date == date &&
                        !x.IsBooked)
            .ToListAsync();
        }
        public async Task<Slot> GetByIdAsync(int id)
        {
         return   await _context.Slots.FindAsync(id);
        }

        public async Task AddSlotAsync(Slot slot)
        {
            await _context.Slots.AddAsync(slot);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateSlotAsync(Slot slot)
        {
            _context.Slots.Update(slot);
            await _context.SaveChangesAsync();
        }
       
    }
}
