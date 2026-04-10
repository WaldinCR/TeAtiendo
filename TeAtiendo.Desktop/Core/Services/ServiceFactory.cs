using TeAtiendo.Desktop.Services;

namespace TeAtiendo.Desktop.Core.Services
{
    public sealed class ServiceFactory
    {

        private const string BaseUrl = "http://localhost:5067/api";

        public ApiService ApiService() => new ApiService(BaseUrl);

        public AuthService AuthService() => new AuthService(ApiService());

        public RestauranteService RestauranteService() => new RestauranteService(ApiService());
        public MenuService MenuService() => new MenuService(ApiService());
        public ReservaService ReservaService() => new ReservaService(ApiService());
        public OrdenService OrdenService() => new OrdenService(ApiService());
        public NotificacionService NotificacionService() => new NotificacionService(ApiService());
        public CategoriaService CategoriaService() => new CategoriaService(ApiService());
    }
}