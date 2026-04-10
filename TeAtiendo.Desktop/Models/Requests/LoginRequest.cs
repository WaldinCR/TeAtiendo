using System.ComponentModel.DataAnnotations;

namespace TeAtiendo.Desktop.Models.Requests
{
    public sealed class LoginRequest
    {
        [Required, EmailAddress]
        public string Correo { get; set; } = "";

        [Required, MinLength(8)]
        public string Password { get; set; } = "";
    }
}