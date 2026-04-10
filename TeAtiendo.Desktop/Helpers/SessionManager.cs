using TeAtiendo.Desktop.Models.Responses;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Desktop.Helpers
{
    public static class SessionManager
    {
        public static string? JwtToken { get; private set; }
        public static UsuarioResponse? UsuarioActual { get; private set; }

        // Nuevo: Restaurante seleccionado para filtrar módulos de "Restaurante"
        public static Guid RestauranteActualId { get; private set; } = Guid.Empty;
        public static string RestauranteActualNombre { get; private set; } = "";

        public static bool IsAuthenticated => !string.IsNullOrWhiteSpace(JwtToken);
        public static bool EsAdmin => UsuarioActual?.Rol == RolUsuario.Admin;

        public static void SetSession(string token, UsuarioResponse? user)
        {
            JwtToken = token;
            UsuarioActual = user;

            // Al iniciar sesión reseteamos el restaurante seleccionado
            RestauranteActualId = Guid.Empty;
            RestauranteActualNombre = "";
        }

        public static void SetRestauranteActual(Guid id, string nombre)
        {
            RestauranteActualId = id;
            RestauranteActualNombre = nombre ?? "";
        }

        public static void ClearRestauranteActual()
        {
            RestauranteActualId = Guid.Empty;
            RestauranteActualNombre = "";
        }

        public static void Clear()
        {
            JwtToken = null;
            UsuarioActual = null;
            ClearRestauranteActual();
        }
    }
}