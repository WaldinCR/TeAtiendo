using Microsoft.EntityFrameworkCore;
using TeAtiendo.Domain.Entities.Admin;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Admin
{
    public class ModeracionContenidoRepository : BaseRepository<ModeracionContenido>, IModeracionContenidoRepository
    {
        public ModeracionContenidoRepository(TeAtiendoContext context) : base(context) { }

        public async Task<IReadOnlyList<ModeracionContenido>> GetByAdminAsync(Guid adminId, CancellationToken ct = default)
        {
            return await _dbSet.Where(m => m.AdminId == adminId && m.Activo)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<ModeracionContenido>> GetByTipoContenidoAsync(string tipoContenido, CancellationToken ct = default)
        {
            return await _dbSet.Where(m => m.TipoContenido == tipoContenido && m.Activo)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<ModeracionContenido>> GetByEstadoAsync(string estado, CancellationToken ct = default)
        {
            return await _dbSet.Where(m => m.Estado == estado && m.Activo)
                .OrderByDescending(m => m.Fecha)
                .ToListAsync(ct);
        }
    }
}