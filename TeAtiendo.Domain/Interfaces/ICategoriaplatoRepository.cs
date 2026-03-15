using TeAtiendo.Domain.Entities.Catalog;

namespace TeAtiendo.Domain.Interfaces
{
    public interface ICategoriaplatoRepository : IRepository<CategoriaPlato>
    {
        Task<IReadOnlyList<CategoriaPlato>> GetByMenuAsync(Guid menuId, CancellationToken ct = default);
    }
}