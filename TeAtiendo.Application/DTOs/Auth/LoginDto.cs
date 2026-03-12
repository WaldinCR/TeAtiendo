namespace TeAtiendo.Application.DTOs.Auth
{
    public sealed class LoginDto
    {
        public string Correo { get; set; } = "";
        public string Password { get; set; } = "";
    }
}