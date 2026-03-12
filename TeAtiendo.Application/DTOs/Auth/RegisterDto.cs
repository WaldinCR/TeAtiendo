using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.DTOs.Auth
{
    public sealed class RegisterDto
    {
        public string Nombre { get; set; } = "";
        public string Correo { get; set; } = "";
        public string Password { get; set; } = "";
        public RolUsuario Rol { get; set; }
    }
}