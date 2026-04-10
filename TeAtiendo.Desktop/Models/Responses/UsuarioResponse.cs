using TeAtiendo.Desktop.Models;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Desktop.Models.Responses
{
    public sealed class UsuarioResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Correo { get; set; } = "";
        public RolUsuario Rol { get; set; }
    }
}