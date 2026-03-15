using Microsoft.EntityFrameworkCore;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Catalogo
{
    public class PlatoRepository : BaseRepository<Plato>, IPlatoRepository
    {
        public PlatoRepository(TeAtiendoContext context) : base(context) { }

        public async Task<IReadOnlyList<Plato>> ObtenerPorMenuAsync(Guid menuId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(x => x.MenuId == menuId && x.Activo)
                .AsNoTracking()
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Plato>> GetByCategoriaAsync(Guid categoriaId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(x => x.CategoriaPlatoId == categoriaId && x.Activo)
                .OrderBy(x => x.Nombre)
                .AsNoTracking()
                .ToListAsync(ct);
        }
    }
}