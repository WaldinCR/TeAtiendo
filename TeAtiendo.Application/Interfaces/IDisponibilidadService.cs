using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;

namespace TeAtiendo.Application.Interfaces
{
    public interface IDisponibilidadService : IBaseService<DisponibilidadDto>
    {
        Task<IReadOnlyList<DisponibilidadDto>> GetByRestauranteAsync(Guid restauranteId, CancellationToken ct = default);
        Task<IReadOnlyList<DisponibilidadDto>> GetByFechaAsync(DateTime fecha, CancellationToken ct = default);
        Task<IReadOnlyList<DisponibilidadDto>> GetByRestauranteAndFechaAsync(Guid restauranteId, DateTime fecha, CancellationToken ct = default);
    }
}