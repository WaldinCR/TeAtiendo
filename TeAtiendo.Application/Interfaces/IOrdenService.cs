using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.DTOs.Orden;

namespace TeAtiendo.Application.Interfaces
{
    public interface IOrdenService : IBaseService<OrdenDto, SaveOrdenDto, UpdateOrdenDto>
    {
        Task<IEnumerable<OrdenDto>> GetOrdenesPorUsuarioAsync(Guid usuarioId);

        Task<IEnumerable<OrdenDto>> GetOrdenesPorRestauranteAsync(Guid restauranteId);

        Task<bool> CambiarEstadoOrdenAsync(Guid ordenId, string estado);
    }
}