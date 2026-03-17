using TeAtiendo.Application.DTOs.Menu;

namespace TeAtiendo.Application.Interfaces
{
    public interface IMenuService
    {
        Task<IReadOnlyList<MenuDto>> GetAllAsync(CancellationToken ct = default);
        Task<MenuDto?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<MenuDto> CreateAsync(CreateMenuDto dto, CancellationToken ct = default);
        Task<MenuDto> UpdateAsync(Guid id, UpdateMenuDto dto, CancellationToken ct = default);
        Task DeleteAsync(Guid id, CancellationToken ct = default);
    }
}