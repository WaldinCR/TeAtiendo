using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Orden;

namespace TeAtiendo.Application.Interfaces
{
    public interface IOrdenService : IBaseService<OrdenDto>
    {

        Task<bool> DeleteAsync(RemoveOrdenDto dto, CancellationToken ct = default);
    }
}