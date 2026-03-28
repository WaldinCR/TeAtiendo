namespace TeAtiendo.Web.Models.Requets
{
    public class RegisterRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int Rol { get; set; } = 1;
    }
}