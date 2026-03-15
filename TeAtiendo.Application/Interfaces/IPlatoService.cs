using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Domain.Interfaces;

namespace TeAtiendo.Application.Interfaces
{
    public interface IPlatoService : IBaseService<PlatoDto>
    {
        Task<IReadOnlyList<PlatoDto>> GetByCategoriaAsync(Guid categoriaId, CancellationToken ct = default);
        Task<IReadOnlyList<PlatoDto>> ObtenerPorMenuAsync(Guid menuId, CancellationToken ct = default);
    }
}