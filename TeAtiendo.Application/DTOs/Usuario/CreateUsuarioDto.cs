using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.DTOs.Usuario
{
    public sealed class CreateUsuarioDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; }
    }
}