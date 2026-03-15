using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Social;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Interface;

namespace TeAtiendo.Application.Services
{
    public sealed class NotificacionService : BaseService<Notificacion, NotificacionDto>, INotificacionService
    {
        private readonly INotificacionRepository _notificacionRepository;

        public NotificacionService(IUnitOfWork uow, INotificacionRepository notificacionRepository)
            : base(notificacionRepository, uow)
        {
            _notificacionRepository = notificacionRepository;
        }

        protected override NotificacionDto ToDto(Notificacion e) => new()
        {
            Id = e.Id,
            UsuarioId = e.UsuarioId,
            Titulo = e.Tipo,
            Mensaje = e.Mensaje,
            Tipo = e.Tipo,
            Leida = e.Leida,
            FechaCreacion = e.FechaEnvio
        };

        protected override void ApplyDto(NotificacionDto dto, Notificacion e)
        {
            if (dto.UsuarioId == Guid.Empty) throw new ArgumentException("UsuarioId requerido");
            if (string.IsNullOrWhiteSpace(dto.Mensaje)) throw new ArgumentException("Mensaje requerido");

            e.UsuarioId = dto.UsuarioId;
            e.Tipo = dto.Tipo ?? "general";
            e.Mensaje = dto.Mensaje.Trim();
            e.Leida = dto.Leida;
        }

        public async Task<IReadOnlyList<NotificacionDto>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default)
        {
            var notificaciones = await _notificacionRepository.GetByUsuarioAsync(usuarioId, ct);
            return notificaciones.Select(ToDto).ToList();
        }

        public async Task<IReadOnlyList<NotificacionDto>> GetNoLeidasByUsuarioAsync(Guid usuarioId, CancellationToken ct = default)
        {
            var notificaciones = await _notificacionRepository.GetNoLeidasByUsuarioAsync(usuarioId, ct);
            return notificaciones.Select(ToDto).ToList();
        }

        public async Task<bool> MarkAsReadAsync(Guid notificacionId, CancellationToken ct = default)
        {
            await _notificacionRepository.MarkAsReadAsync(notificacionId, ct);
            await Uow.SaveAsync(ct);
            return true;
        }
    }
}