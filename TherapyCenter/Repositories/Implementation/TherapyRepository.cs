using Microsoft.EntityFrameworkCore;
using TherapyCenter.Data;
using TherapyCenter.Entities;
using TherapyCenter.Repositories.Interfaces;

namespace TherapyCenter.Repositories.Implementation
{
    public class TherapyRepository: ITherapyRepository
    {
        private readonly AppDbContext _context;

        public TherapyRepository(AppDbContext context)
        {
            _context = context;
        }
       public async Task<Therapy> GetByIdAsync(int id)
        {
            return await _context.Therapies.FindAsync(id);
        }
       public async Task<List<Therapy>> GetAllAsync()
        {
            return await _context.Therapies.ToListAsync();
        }
       public async Task AddAsync(Therapy therapy)
        {
            await _context.Therapies.AddAsync(therapy);
            await _context.SaveChangesAsync();

        }
       public async Task UpdateAsync(Therapy therapy)
        {
            _context.Therapies.Update(therapy);
            await _context.SaveChangesAsync();
        }
       public async Task DeleteAsync(int id)
        {
            var therapy = await GetByIdAsync(id);

            if (therapy != null)
            {
                 _context.Therapies.Remove(therapy);
                await _context.SaveChangesAsync();
            }
        }
    }
}
