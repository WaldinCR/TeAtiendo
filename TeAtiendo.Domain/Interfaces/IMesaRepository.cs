using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IMesaRepository : IRepository<Mesa>
    {
        Task<IReadOnlyList<Mesa>> GetByRestauranteAsync(Guid restauranteId, CancellationToken ct = default);
        Task<Mesa?> GetByRestauranteAndNumeroAsync(Guid restauranteId, int numero, CancellationToken ct = default);
    }
}