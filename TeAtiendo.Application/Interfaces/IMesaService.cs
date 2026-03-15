using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;

namespace TeAtiendo.Application.Interfaces
{
    public interface IMesaService : IBaseService<MesaDto>
    {
        Task<IReadOnlyList<MesaDto>> GetByRestauranteAsync(Guid restauranteId, CancellationToken ct = default);
        Task<MesaDto?> GetByRestauranteAndNumeroAsync(Guid restauranteId, int numero, CancellationToken ct = default);
    }
}