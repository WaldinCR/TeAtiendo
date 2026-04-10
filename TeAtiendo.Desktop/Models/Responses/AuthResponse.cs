namespace TeAtiendo.Desktop.Models.Responses
{
    public sealed class AuthResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; } = "";
        public string Token { get; set; } = "";
        public UsuarioResponse? User { get; set; }
    }
}