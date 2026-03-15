using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace TeAtiendo.Persistence.Repositories.Catalog
{
    public class CategoriaplatoRepository : BaseRepository<CategoriaPlato>, ICategoriaplatoRepository
    {
        public CategoriaplatoRepository(TeAtiendoContext context) : base(context)
        {
        }

        public async Task<IReadOnlyList<CategoriaPlato>> GetByMenuAsync(Guid menuId, CancellationToken ct = default)
        {
            return await _dbSet
                .Where(c => c.MenuId == menuId && c.Activo)
                .Include(c => c.Platos)
                .ToListAsync(ct);
        }
    }
}