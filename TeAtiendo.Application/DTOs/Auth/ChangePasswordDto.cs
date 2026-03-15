namespace TeAtiendo.Application.DTOs
{
    public class ChangePasswordDto
    {
        public Guid UsuarioId { get; set; }
        public string PasswordActual { get; set; } = string.Empty;
        public string PasswordNueva { get; set; } = string.Empty;
    }
}