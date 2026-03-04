using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Catalogo
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {
        public MenuRepository(TeAtiendoContext context) : base(context) { }
    }
}