using TeAtiendo.Desktop.Services;

namespace TeAtiendo.Desktop.Core.Services;

public sealed class ServiceFactory
{
    public ApiService Api { get; }

    public AuthService Auth { get; }
    public UsuarioService Usuarios { get; }
    public NotificacionService Notificaciones { get; }

    public ServiceFactory()
    {
        Api = new ApiService();

        Auth = new AuthService(Api);
        Usuarios = new UsuarioService(Api);
        Notificaciones = new NotificacionService(Api);

    }
}