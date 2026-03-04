using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Catalogo
{
    public class PlatoRepository : BaseRepository<Plato>, IPlatoRepository
    {
        public PlatoRepository(TeAtiendoContext context) : base(context) { }

        public async Task<IEnumerable<Plato>> ObtenerPorMenuAsync(Guid menuId)
        {
            return await _dbSet
                .Where(x => x.MenuId == menuId && x.Activo)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}