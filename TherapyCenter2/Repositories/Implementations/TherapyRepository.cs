using TherapyCenter2.Data;
using TherapyCenter2.Models;
using TherapyCenter2.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TherapyCenter2.Repositories.Implementations
{
    public class TherapyRepository: ITherapyRepository
    {

        private readonly AppDbContext _context;

        public TherapyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Therapy> AddAsync(Therapy therapy)
        {
            _context.Therapies.Add(therapy);
            await _context.SaveChangesAsync();
            return therapy;
        }

        public async Task<List<Therapy>> GetAllAsync()
        {
            return await _context.Therapies.ToListAsync();
        }

        public async Task<Therapy?> GetByIdAsync(int id)
        {
            return await _context.Therapies.FindAsync(id);
        }

        public async Task UpdateAsync(Therapy therapy)
        {
            _context.Therapies.Update(therapy);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Therapy therapy)
        {
            _context.Therapies.Remove(therapy);
            await _context.SaveChangesAsync();
        }
    }
}
