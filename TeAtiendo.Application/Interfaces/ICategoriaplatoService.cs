using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;

namespace TeAtiendo.Application.Interfaces
{
    public interface ICategoriaplatoService : IBaseService<CategoriaplatoDto>
    {
        Task<IReadOnlyList<CategoriaplatoDto>> GetByMenuAsync(Guid menuId, CancellationToken ct = default);
    }
}