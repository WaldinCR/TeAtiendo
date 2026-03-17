using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Pago;

namespace TeAtiendo.Application.Interfaces
{
    public interface IPagoService : IBaseService<PagoDto>
    {
        Task<PagoDto?> GetByOrdenIdAsync(Guid ordenId, CancellationToken ct = default);
    }
}