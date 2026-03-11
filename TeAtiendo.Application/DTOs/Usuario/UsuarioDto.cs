using System;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.DTOs.Usuario
{
    public class UsuarioDto
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public RolUsuario Rol { get; set; }
        public string Estado { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }
}