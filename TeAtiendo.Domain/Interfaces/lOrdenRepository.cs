using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IOrdenRepository : IRepository<Orden>
    {
        Task<IReadOnlyList<Orden>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default);
    }
}