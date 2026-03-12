using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Restaurante;

namespace TeAtiendo.Application.Interfaces
{
    public interface IRestauranteService : IBaseService<RestauranteDto>
    {
        Task<IReadOnlyList<RestauranteDto>> BuscarPorNombreAsync(string nombre, CancellationToken ct = default);
    }
}