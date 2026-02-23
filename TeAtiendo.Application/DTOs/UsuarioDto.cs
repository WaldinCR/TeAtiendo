using System;

namespace TeAtiendo.Application.DTOs
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;

        // roles del sistema
        public string Rol { get; set; } = "Cliente";

        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
        public string Estado { get; set; } = "Activo";
    }

    public class RegisterRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = "Cliente";
    }

    public class LoginRequest
    {
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class AuthResult
    {
        public int IdUsuario { get; set; }
        public string Rol { get; set; } = string.Empty;

       
        public string TokenSimulado { get; set; } = string.Empty;
    }
}
