using TeAtiendo.Domain.Entities.Social;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace TeAtiendo.Persistence.Repositories.Social
{
    public class NotificacionRepository : BaseRepository<Notificacion>, INotificacionRepository
    {
        public NotificacionRepository(TeAtiendoContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Notificacion>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(n => n.UsuarioId == usuarioId && n.Activo)
                .OrderByDescending(n => n.FechaEnvio)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Notificacion>> GetNoLeidasByUsuarioAsync(Guid usuarioId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(n => n.UsuarioId == usuarioId && !n.Leida && n.Activo)
                .OrderByDescending(n => n.FechaEnvio)
                .ToListAsync(ct);
        }

        public async Task MarkAsReadAsync(Guid notificacionId, CancellationToken ct = default)
        {
            var notificacion = await GetByIdAsync(notificacionId, ct);
            if (notificacion is null) return;

            notificacion.Leida = true;
            await UpdateAsync(notificacion, ct);
        }
    }
}