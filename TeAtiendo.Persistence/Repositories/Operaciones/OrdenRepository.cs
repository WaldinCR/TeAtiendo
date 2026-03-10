using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Operaciones
{
    public class OrdenRepository : BaseRepository<Orden>, IOrdenRepository
    {
        public OrdenRepository(TeAtiendoContext context) : base(context) { }

        public async Task<IEnumerable<Orden>> GetByUsuarioAsync(Guid usuarioId)
        {
            return await _dbSet
                .Where(o => o.UsuarioId == usuarioId && o.Activo)
                .Include(o => o.Pago)
                .Include(o => o.Detalles)
                .ToListAsync();
        }
    }
}