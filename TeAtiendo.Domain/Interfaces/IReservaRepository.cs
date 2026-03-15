using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IReservaRepository : IRepository<Reserva>
    {
        Task<IReadOnlyList<Reserva>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default);
    }
}