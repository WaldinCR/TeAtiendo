using TeAtiendo.Desktop.Services;

namespace TeAtiendo.Desktop.Helpers;

public static class AppBootstrapper
{
    private static readonly Lazy<ApiService> _api = new(() => new ApiService());

    public static ApiService Api => _api.Value;

    public static AuthService Auth => new(Api);

    public static UsuarioService Usuarios => new(Api);

    public static RestauranteService Restaurantes => new(Api);

    public static MenuService Menus => new(Api);

    public static CategoriaService Categorias => new(Api);

    public static PlatoService Platos => new(Api);

    public static MesaService Mesas => new(Api);

    public static DisponibilidadService Disponibilidades => new(Api);

    public static ReservaService Reservas => new(Api);

    public static OrdenService Ordenes => new(Api);

    public static PagoService Pagos => new(Api);

    public static NotificacionService Notificaciones => new(Api);

    public static AuditoriaService Auditorias => new(Api);

    public static ModeracionService Moderacion => new(Api);
}