using Microsoft.EntityFrameworkCore;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Operaciones
{
    public class PagoRepository : BaseRepository<Pago>, IPagoRepository
    {
        public PagoRepository(TeAtiendoContext context) : base(context) { }

        public async Task<Pago?> GetByOrdenIdAsync(Guid ordenId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(p => p.OrdenId == ordenId && p.Activo)
                .FirstOrDefaultAsync(ct);
        }
    }
}