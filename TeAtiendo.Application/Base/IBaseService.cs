namespace TeAtiendo.Application.Base
{
    public interface IBaseService<TDto>
    {
        Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken ct = default);
        Task<TDto?> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<TDto> CreateAsync(TDto dto, CancellationToken ct = default);
        Task<TDto?> UpdateAsync(Guid id, TDto dto, CancellationToken ct = default);

        Task<bool> DeleteAsync(Guid id, Guid userId, CancellationToken ct = default);
    }
}