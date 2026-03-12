using Microsoft.EntityFrameworkCore;
using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Seguridad
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(TeAtiendoContext context) : base(context) { }

        public async Task<Usuario?> GetByCorreoAsync(string correo, CancellationToken ct = default)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Correo == correo && u.Activo, ct);
        }
    }
}