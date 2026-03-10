using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.DTOs.Restaurante;

namespace TeAtiendo.Application.Interfaces
{
    public interface IRestauranteService : IBaseService<RestauranteDto, RestauranteDto, RestauranteDto>
    {
        Task<IEnumerable<RestauranteDto>> BuscarPorNombreAsync(string nombre);

        Task<IEnumerable<RestauranteDto>> GetRestaurantesActivosAsync();

        Task<bool> CambiarEstadoAsync(Guid id, bool activo);
    }
}