using System;

namespace TeAtiendo.Application.DTOs
{
    public class RestauranteDto
    {
        public int IdRestaurante { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        public string HoraApertura { get; set; } = "09:00";
        public string HoraCierre { get; set; } = "22:00";

        public string Estado { get; set; } = "Activo";
    }

    public class RestauranteCreateRequest
    {
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string HoraApertura { get; set; } = "09:00";
        public string HoraCierre { get; set; } = "22:00";
    }
}
