using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Helpers
{
    public static class SessionManager
    {
        public static string? Token { get; private set; }
        public static Usuario? UsuarioActual { get; private set; }
        public static int? RestauranteIdActual { get; set; }

        public static void IniciarSesion(string token, Usuario usuario)
        {
            Token = token;
            UsuarioActual = usuario;
        }

        public static void CerrarSesion()
        {
            Token = null;
            UsuarioActual = null;
            RestauranteIdActual = null;
        }

        public static bool EstaAutenticado => !string.IsNullOrEmpty(Token) && UsuarioActual != null;

        public static bool EsAdmin => UsuarioActual?.Rol == (int)RolUsuario.Admin;

        public static bool EsRestaurante => UsuarioActual?.Rol == (int)RolUsuario.Restaurante;
    }
}