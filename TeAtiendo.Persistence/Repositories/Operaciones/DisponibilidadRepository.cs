using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace TeAtiendo.Persistence.Repositories.Operaciones
{
    public class DisponibilidadRepository : BaseRepository<Disponibilidad>, IDisponibilidadRepository
    {
        public DisponibilidadRepository(TeAtiendoContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<Disponibilidad>> GetByRestauranteAsync(Guid restauranteId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(d => d.RestauranteId == restauranteId && d.Activo)
                .OrderBy(d => d.Fecha)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Disponibilidad>> GetByFechaAsync(DateTime fecha, CancellationToken ct = default)
        {
            var fecha_only = DateOnly.FromDateTime(fecha);
            return await _dbSet
                .Where(d => DateOnly.FromDateTime(d.Fecha) == fecha_only && d.Activo)
                .OrderBy(d => d.HoraInicio)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Disponibilidad>> GetByRestauranteAndFechaAsync(Guid restauranteId, DateTime fecha, CancellationToken ct = default)
        {
            var fecha_only = DateOnly.FromDateTime(fecha);
            return await _dbSet
                .Where(d => d.RestauranteId == restauranteId && DateOnly.FromDateTime(d.Fecha) == fecha_only && d.Activo)
                .OrderBy(d => d.HoraInicio)
                .ToListAsync(ct);
        }
    }
}