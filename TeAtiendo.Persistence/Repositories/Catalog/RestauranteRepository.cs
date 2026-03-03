using Microsoft.EntityFrameworkCore;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Catalog
{
    public class RestauranteRepository
    {
        private readonly TeAtiendoContext _context;

        public RestauranteRepository(TeAtiendoContext context)
        {
            _context = context;
        }

        public async Task<List<Restaurante>> GetAllAsync()
        {
            return await _context.Restaurantes.ToListAsync();
        }

        public async Task<Restaurante?> GetByIdAsync(int id)
        {
            return await _context.Restaurantes
                                 .Include(r => r.Menus)
                                 .FirstOrDefaultAsync(r => r.IdRestaurante == id);
        }

        public async Task AddAsync(Restaurante restaurante)
        {
            await _context.Restaurantes.AddAsync(restaurante);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Restaurante restaurante)
        {
            _context.Restaurantes.Update(restaurante);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var restaurante = await GetByIdAsync(id);
            if (restaurante != null)
            {
                _context.Remove(restaurante);
                await _context.SaveChangesAsync();
            }
        }
    }
}