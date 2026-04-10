using System.ComponentModel.DataAnnotations;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Desktop.Models.Requests
{
    public sealed class RegisterRequest
    {
        [Required, MinLength(3)]
        public string Nombre { get; set; } = "";

        [Required, EmailAddress]
        public string Correo { get; set; } = "";

        [Required, MinLength(8)]
        public string Password { get; set; } = "";

        [Required]
        public RolUsuario Rol { get; set; } = RolUsuario.Cliente;
    }
}