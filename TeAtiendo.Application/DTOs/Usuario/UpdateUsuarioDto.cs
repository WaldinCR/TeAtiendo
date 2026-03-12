using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.DTOs.Usuario
{
    public sealed class UpdateUsuarioDto
    {
        public string Nombre { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; }
    }
}