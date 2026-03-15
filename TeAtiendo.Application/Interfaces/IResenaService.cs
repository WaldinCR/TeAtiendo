using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;

namespace TeAtiendo.Application.Interfaces
{
    public interface IResenaService : IBaseService<ResenaDto>
    {
        Task<IReadOnlyList<ResenaDto>> GetByRestauranteAsync(Guid restauranteId, CancellationToken ct = default);
        Task<IReadOnlyList<ResenaDto>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default);
        Task<double> GetPromediaCalificacionAsync(Guid restauranteId, CancellationToken ct = default);
    }
}