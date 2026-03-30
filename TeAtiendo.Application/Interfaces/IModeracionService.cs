using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Moderacion;

namespace TeAtiendo.Application.Interfaces
{
    public interface IModeracionService : IBaseService<ModeracionContenidoDto>
    {
        Task<IReadOnlyList<ModeracionContenidoDto>> GetByAdminAsync(Guid adminId, CancellationToken ct = default);
        Task<IReadOnlyList<ModeracionContenidoDto>> GetByTipoContenidoAsync(string tipoContenido, CancellationToken ct = default);
        Task<IReadOnlyList<ModeracionContenidoDto>> GetByEstadoAsync(string estado, CancellationToken ct = default);
        Task<ModeracionContenidoDto?> CambiarEstadoAsync(Guid id, string nuevoEstado, CancellationToken ct = default);
    }
}