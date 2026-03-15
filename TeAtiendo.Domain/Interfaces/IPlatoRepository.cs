using TeAtiendo.Domain.Entities.Catalog;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IPlatoRepository : IRepository<Plato>
    {
        Task<IReadOnlyList<Plato>> ObtenerPorMenuAsync(Guid menuId, CancellationToken ct = default);
        Task<IReadOnlyList<Plato>> GetByCategoriaAsync(Guid categoriaId, CancellationToken ct = default);
    }
}