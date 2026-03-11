using Microsoft.EntityFrameworkCore;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Operaciones
{
    public class OrdenRepository : BaseRepository<Orden>, IOrdenRepository
    {
        public OrdenRepository(TeAtiendoContext context) : base(context) { }

        public async Task<IReadOnlyList<Orden>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(o => o.UsuarioId == usuarioId && o.Activo)
                .Include(o => o.Pago)
                .Include(o => o.Detalles)
                .ToListAsync(ct);
        }
    }
}