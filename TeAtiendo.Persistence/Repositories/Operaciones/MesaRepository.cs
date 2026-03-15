using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace TeAtiendo.Persistence.Repositories.Operaciones
{
    public class MesaRepository : BaseRepository<Mesa>, IMesaRepository
    {
        public MesaRepository(TeAtiendoContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Mesa>> GetByRestauranteAsync(Guid restauranteId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(m => m.RestauranteId == restauranteId && m.Activo)
                .OrderBy(m => m.Numero)
                .ToListAsync(ct);
        }

        public async Task<Mesa?> GetByRestauranteAndNumeroAsync(Guid restauranteId, int numero, CancellationToken ct = default)
        {
            return await _dbSet
                .FirstOrDefaultAsync(m => m.RestauranteId == restauranteId && m.Numero == numero && m.Activo, ct);
        }
    }
}