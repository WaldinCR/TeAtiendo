using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Catalog
{
    public class MenuRepository
    {
        private readonly TeAtiendoContext _context;

        public MenuRepository(TeAtiendoContext context)
        {
            _context = context;
        }

        public async Task<List<Menu>> GetByRestauranteAsync(int restauranteId)
        {
            return await _context.Set<Menu>()
                                 .Where(m => m.IdRestaurante == restauranteId)
                                 .Include(m => m.Platos)
                                 .ToListAsync();
        }

        public async Task<Menu?> GetByIdAsync(int id)
        {
            return await _context.Set<Menu>()
                                 .Include(m => m.Platos)
                                 .FirstOrDefaultAsync(m => m.IdMenu == id);
        }

        public async Task AddAsync(Menu menu)
        {
            await _context.Set<Menu>().AddAsync(menu);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Menu menu)
        {
            _context.Update(menu);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var menu = await GetByIdAsync(id);
            if (menu != null)
            {
                _context.Remove(menu);
                await _context.SaveChangesAsync();
            }
        }
    }
}