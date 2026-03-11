using System;

namespace TeAtiendo.Application.DTOs.Restaurante
{
    public class RestauranteDto
    {
        public int IdRestaurante { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        public string Telefono { get; set; } = string.Empty;

        public string Correo { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;

        public TimeOnly HorarioApertura { get; set; }

        public TimeOnly HorarioCierre { get; set; }
    }
}