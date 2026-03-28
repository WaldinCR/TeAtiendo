using TeAtiendo.Application.DTOs.Auditoria;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Auditory;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Interface;

namespace TeAtiendo.Application.Services
{
    public sealed class AuditoriaService : IAuditoriaService
    {
        private readonly IUnitOfWork _uow;
        private readonly IAuditoriaRepository _auditoriaRepository;

        public AuditoriaService(IUnitOfWork uow, IAuditoriaRepository auditoriaRepository)
        {
            _uow = uow;
            _auditoriaRepository = auditoriaRepository;
        }

        private static AuditoriaDto ToDto(Auditoria e) => new()
        {
            Id = e.Id,
            AdminId = e.AdminId,
            Fecha = e.Fecha,
            Accion = e.Accion,
            Modulo = e.Modulo,
            Detalle = e.Detalle,
            Ip = e.Ip,
            FechaCreacion = e.FechaCreacion
        };

        public async Task<IReadOnlyList<AuditoriaDto>> GetAllAsync(CancellationToken ct = default)
        {
            var auditorias = await _auditoriaRepository.GetAllAsync(ct);
            return auditorias.Select(ToDto).ToList();
        }

        public async Task<AuditoriaDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var auditoria = await _auditoriaRepository.GetByIdAsync(id, ct);
            return auditoria is null ? null : ToDto(auditoria);
        }

        public async Task<IReadOnlyList<AuditoriaDto>> GetByAdminAsync(Guid adminId, CancellationToken ct = default)
        {
            var auditorias = await _auditoriaRepository.GetAllAsync(ct);
            return auditorias
                .Where(a => a.AdminId == adminId)
                .Select(ToDto)
                .ToList();
        }

        public async Task<IReadOnlyList<AuditoriaDto>> GetByModuloAsync(string modulo, CancellationToken ct = default)
        {
            var auditorias = await _auditoriaRepository.GetAllAsync(ct);
            return auditorias
                .Where(a => a.Modulo.Equals(modulo, StringComparison.OrdinalIgnoreCase))
                .Select(ToDto)
                .ToList();
        }

        public async Task<IReadOnlyList<AuditoriaDto>> GetByFechaRangoAsync(DateTime desde, DateTime hasta, CancellationToken ct = default)
        {
            var auditorias = await _auditoriaRepository.GetAllAsync(ct);
            return auditorias
                .Where(a => a.Fecha >= desde && a.Fecha <= hasta)
                .Select(ToDto)
                .ToList();
        }

        public async Task<AuditoriaDto> RegistrarAsync(AuditoriaDto dto, CancellationToken ct = default)
        {
            if (dto.AdminId == Guid.Empty)
                throw new ArgumentException("AdminId es requerido");
            if (string.IsNullOrWhiteSpace(dto.Accion))
                throw new ArgumentException("Acción es requerida");
            if (string.IsNullOrWhiteSpace(dto.Modulo))
                throw new ArgumentException("Módulo es requerido");

            var entity = new Auditoria
            {
                Id = Guid.NewGuid(),
                AdminId = dto.AdminId,
                Fecha = DateTime.UtcNow,
                Accion = dto.Accion.Trim(),
                Modulo = dto.Modulo.Trim(),
                Detalle = dto.Detalle?.Trim() ?? string.Empty,
                Ip = dto.Ip?.Trim() ?? string.Empty
            };

            await _auditoriaRepository.AddAsync(entity, ct);
            await _uow.SaveAsync(ct);

            return ToDto(entity);
        }
    }
}