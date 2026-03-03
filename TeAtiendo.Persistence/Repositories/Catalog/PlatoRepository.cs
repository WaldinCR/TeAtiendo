using Microsoft.EntityFrameworkCore;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Catalog
{
    public class PlatoRepository
    {
        private readonly TeAtiendoContext _context;

        public PlatoRepository(TeAtiendoContext context)
        {
            _context = context;
        }

        public async Task<List<Plato>> GetByMenuAsync(int menuId)
        {
            return await _context.Set<Plato>()
                                 .Where(p => p.IdMenu == menuId)
                                 .ToListAsync();
        }

        public async Task<Plato?> GetByIdAsync(int id)
        {
            return await _context.Set<Plato>()
                                 .FirstOrDefaultAsync(p => p.IdPlato == id);
        }

        public async Task AddAsync(Plato plato)
        {
            await _context.Set<Plato>().AddAsync(plato);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Plato plato)
        {
            _context.Update(plato);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var plato = await GetByIdAsync(id);
            if (plato != null)
            {
                _context.Remove(plato);
                await _context.SaveChangesAsync();
            }
        }
    }
}