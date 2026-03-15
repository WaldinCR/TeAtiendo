using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IDisponibilidadRepository : IRepository<Disponibilidad>
    {
        Task<IReadOnlyList<Disponibilidad>> GetByRestauranteAsync(Guid restauranteId, CancellationToken ct = default);
        Task<IReadOnlyList<Disponibilidad>> GetByFechaAsync(DateTime fecha, CancellationToken ct = default);
        Task<IReadOnlyList<Disponibilidad>> GetByRestauranteAndFechaAsync(Guid restauranteId, DateTime fecha, CancellationToken ct = default);
    }
}