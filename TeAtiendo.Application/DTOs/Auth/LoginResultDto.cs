using TeAtiendo.Application.DTOs.Usuario;

namespace TeAtiendo.Application.DTOs.Auth
{
    public sealed class LoginResultDto
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public UsuarioDto? User { get; set; }
    }
}