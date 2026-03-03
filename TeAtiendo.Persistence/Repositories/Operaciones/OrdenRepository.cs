using Microsoft.EntityFrameworkCore;
using System.Windows.Controls;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Operaciones
{
    public class OrdenRepository
    {
        private readonly TeAtiendoContext _context;

        public OrdenRepository(TeAtiendoContext context)
        {
            _context = context;
        }

        public async Task<List<Orden>> GetByUsuarioAsync(int usuarioId)
        {
            return await _context.Set<Orden>()
                                 .Where(o => o.IdUsuario == usuarioId)
                                 .Include(o => o.Pago)
                                 .ToListAsync();
        }

        public async Task<Orden?> GetByIdAsync(int id)
        {
            return await _context.Set<Orden>()
                                 .Include(o => o.Pago)
                                 .FirstOrDefaultAsync(o => o.IdOrden == id);
        }

        public async Task AddAsync(Orden orden)
        {
            await _context.Set<Orden>().AddAsync(orden);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Orden orden)
        {
            _context.Update(orden);
            await _context.SaveChangesAsync();
        }
    }
}