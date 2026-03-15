using TeAtiendo.Domain.Entities.Social;

namespace TeAtiendo.Domain.Interfaces
{
    public interface INotificacionRepository : IRepository<Notificacion>
    {
        Task<IReadOnlyList<Notificacion>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default);
        Task<IReadOnlyList<Notificacion>> GetNoLeidasByUsuarioAsync(Guid usuarioId, CancellationToken ct = default);
        Task MarkAsReadAsync(Guid notificacionId, CancellationToken ct = default);
    }
}