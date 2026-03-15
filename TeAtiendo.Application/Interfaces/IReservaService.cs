using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Reserva;

namespace TeAtiendo.Application.Interfaces
{
    public interface IReservaService : IBaseService<ReservaDto>
    {
        Task<IReadOnlyList<ReservaDto>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default);
        Task<bool> CancelarReservaAsync(Guid reservaId, Guid userId, CancellationToken ct = default);
    }
}