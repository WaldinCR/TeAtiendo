using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Catalog
{
    public class RestauranteRepository : BaseRepository<Restaurante>, IRestauranteRepository
    {
        public RestauranteRepository(TeAtiendoContext context) : base(context) { }
    }
}