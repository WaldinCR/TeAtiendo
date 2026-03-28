using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Moderacion;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Admin;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Interface;

namespace TeAtiendo.Application.Services
{
    public sealed class ModeracionService : BaseService<ModeracionContenido, ModeracionContenidoDto>, IModeracionService
    {
        private readonly IModeracionContenidoRepository _moderacionRepository;

        public ModeracionService(IUnitOfWork uow, IModeracionContenidoRepository moderacionRepository)
            : base(moderacionRepository, uow)
        {
            _moderacionRepository = moderacionRepository;
        }

        protected override ModeracionContenidoDto ToDto(ModeracionContenido e) => new()
        {
            Id = e.Id,
            AdminId = e.AdminId,
            TipoContenido = e.TipoContenido,
            ContenidoId = e.ContenidoId,
            Motivo = e.Motivo,
            Estado = e.Estado,
            Fecha = e.Fecha,
            FechaCreacion = e.FechaCreacion
        };

        protected override void ApplyDto(ModeracionContenidoDto dto, ModeracionContenido e)
        {
            if (dto.AdminId == Guid.Empty)
                throw new ArgumentException("AdminId es requerido");
            if (string.IsNullOrWhiteSpace(dto.TipoContenido))
                throw new ArgumentException("TipoContenido es requerido");
            if (dto.ContenidoId == Guid.Empty)
                throw new ArgumentException("ContenidoId es requerido");

            e.AdminId = dto.AdminId;
            e.TipoContenido = dto.TipoContenido.Trim();
            e.ContenidoId = dto.ContenidoId;
            e.Motivo = dto.Motivo?.Trim() ?? string.Empty;
            e.Estado = dto.Estado ?? "pendiente";
            e.Fecha = dto.Fecha == default ? DateTime.UtcNow : dto.Fecha;
        }

        public async Task<IReadOnlyList<ModeracionContenidoDto>> GetByAdminAsync(Guid adminId, CancellationToken ct = default)
        {
            var items = await _moderacionRepository.GetByAdminAsync(adminId, ct);
            return items.Select(ToDto).ToList();
        }

        public async Task<IReadOnlyList<ModeracionContenidoDto>> GetByTipoContenidoAsync(string tipoContenido, CancellationToken ct = default)
        {
            var items = await _moderacionRepository.GetByTipoContenidoAsync(tipoContenido, ct);
            return items.Select(ToDto).ToList();
        }

        public async Task<IReadOnlyList<ModeracionContenidoDto>> GetByEstadoAsync(string estado, CancellationToken ct = default)
        {
            var items = await _moderacionRepository.GetByEstadoAsync(estado, ct);
            return items.Select(ToDto).ToList();
        }

        public async Task<ModeracionContenidoDto?> CambiarEstadoAsync(Guid id, string nuevoEstado, CancellationToken ct = default)
        {
            var entity = await Repo.GetByIdAsync(id, ct);
            if (entity is null) return null;

            var estadosValidos = new[] { "aprobado", "rechazado", "eliminado" };
            if (!estadosValidos.Contains(nuevoEstado.ToLower()))
                throw new ArgumentException("Estado inválido. Valores permitidos: aprobado, rechazado, eliminado");

            entity.Estado = nuevoEstado.ToLower();
            entity.FechaModificacion = DateTime.UtcNow;

            await Repo.UpdateAsync(entity, ct);
            await Uow.SaveAsync(ct);

            return ToDto(entity);
        }
    }
}