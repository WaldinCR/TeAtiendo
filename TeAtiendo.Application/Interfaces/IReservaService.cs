using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.DTOs.Reserva;

namespace TeAtiendo.Application.Interfaces
{
    public interface IReservaService : IBaseService<ReservaDto, ReservaDto, ReservaDto>
    {
        Task<IEnumerable<ReservaDto>> GetReservasPorUsuarioAsync(Guid usuarioId);

        Task<IEnumerable<ReservaDto>> GetReservasPorRestauranteAsync(Guid restauranteId);

        Task<bool> CancelarReservaAsync(Guid reservaId);
    }
}