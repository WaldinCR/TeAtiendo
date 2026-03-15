using TeAtiendo.Domain.Entities.Social;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace TeAtiendo.Persistence.Repositories.Social
{
    public class ResenaRepository : BaseRepository<Resena>, IResenaRepository
    {
        public ResenaRepository(TeAtiendoContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Resena>> GetByRestauranteAsync(Guid restauranteId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(r => r.RestauranteId == restauranteId && r.Activo)
                .OrderByDescending(r => r.Fecha)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Resena>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(r => r.UsuarioId == usuarioId && r.Activo)
                .OrderByDescending(r => r.Fecha)
                .ToListAsync(ct);
        }

        public async Task<double> GetPromediaCalificacionAsync(Guid restauranteId, CancellationToken ct = default)
        {
            var resenas = await _dbSet
                .Where(r => r.RestauranteId == restauranteId && r.Activo)
                .ToListAsync(ct);

            return resenas.Any() ? resenas.Average(r => r.Calificacion) : 0;
        }
    }
}