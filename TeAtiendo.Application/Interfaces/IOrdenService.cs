using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Orden;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.Interfaces
{
    public interface IOrdenService : IBaseService<OrdenDto>
    {
        Task<IReadOnlyList<OrdenDto>> GetByUsuarioIdAsync(Guid usuarioId, CancellationToken ct = default);
        Task<OrdenDto?> CambiarEstadoAsync(Guid id, EstadoOrden nuevoEstado, CancellationToken ct = default);
        Task<OrdenDto> CreateAsync(SaveOrdenDto dto, CancellationToken ct = default);
    }
}