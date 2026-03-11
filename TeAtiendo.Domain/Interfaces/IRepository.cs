
using TeAtiendo.Domain.Base;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default);

        Task AddAsync(T entity, CancellationToken ct = default);
        Task UpdateAsync(T entity, CancellationToken ct = default);

        Task SoftDeleteAsync(Guid id, Guid userId, CancellationToken ct = default);
    }
}
