using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.DTOs.Pago;

namespace TeAtiendo.Application.Interfaces
{
    public interface IPagoService : IBaseService<PagoDto, PagoDto, PagoDto>
    {
        Task<IEnumerable<PagoDto>> GetPagosPorUsuarioAsync(Guid usuarioId);

        Task<PagoDto?> GetPagoPorOrdenAsync(Guid ordenId);

        Task<bool> ProcesarPagoAsync(Guid ordenId);
    }
}