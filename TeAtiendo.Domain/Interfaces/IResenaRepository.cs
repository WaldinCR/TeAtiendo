using TeAtiendo.Domain.Entities.Social;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IResenaRepository : IRepository<Resena>
    {
        Task<IReadOnlyList<Resena>> GetByRestauranteAsync(Guid restauranteId, CancellationToken ct = default);
        Task<IReadOnlyList<Resena>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default);
        Task<double> GetPromediaCalificacionAsync(Guid restauranteId, CancellationToken ct = default);
    }
}