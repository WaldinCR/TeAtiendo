namespace TeAtiendo.Application.DTOs.Restaurante
{
    public sealed class CreateRestauranteDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;

        public TimeOnly HorarioApertura { get; set; }
        public TimeOnly HorarioCierre { get; set; }
    }
}