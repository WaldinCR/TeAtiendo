using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;

namespace TeAtiendo.Application.Interfaces
{
    public interface INotificacionService : IBaseService<NotificacionDto>
    {
        Task<IReadOnlyList<NotificacionDto>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default);
        Task<IReadOnlyList<NotificacionDto>> GetNoLeidasByUsuarioAsync(Guid usuarioId, CancellationToken ct = default);
        Task<bool> MarkAsReadAsync(Guid notificacionId, CancellationToken ct = default);
    }
}