using TeAtiendo.Application.DTOs.Auditoria;

namespace TeAtiendo.Application.Interfaces
{
    public interface IAuditoriaService
    {
        Task<IReadOnlyList<AuditoriaDto>> GetAllAsync(CancellationToken ct = default);
        Task<AuditoriaDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<AuditoriaDto>> GetByAdminAsync(Guid adminId, CancellationToken ct = default);
        Task<IReadOnlyList<AuditoriaDto>> GetByModuloAsync(string modulo, CancellationToken ct = default);
        Task<IReadOnlyList<AuditoriaDto>> GetByFechaRangoAsync(DateTime desde, DateTime hasta, CancellationToken ct = default);
        Task<AuditoriaDto> RegistrarAsync(AuditoriaDto dto, CancellationToken ct = default);
    }
}