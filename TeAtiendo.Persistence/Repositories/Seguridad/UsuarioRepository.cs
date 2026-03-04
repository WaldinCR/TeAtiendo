using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Seguridad
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(TeAtiendoContext context) : base(context) { }
    }
}