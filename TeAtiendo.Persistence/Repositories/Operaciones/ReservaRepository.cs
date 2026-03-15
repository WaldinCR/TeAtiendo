using Microsoft.EntityFrameworkCore;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Operaciones
{
    public class ReservaRepository : BaseRepository<Reserva>, IReservaRepository
    {
        public ReservaRepository(TeAtiendoContext context) : base(context) { }

        public async Task<IReadOnlyList<Reserva>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(x => x.UsuarioId == usuarioId && x.Activo)
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}