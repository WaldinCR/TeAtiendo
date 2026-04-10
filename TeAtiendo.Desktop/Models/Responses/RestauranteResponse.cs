using System.ComponentModel.DataAnnotations;

namespace TeAtiendo.Desktop.Models.Responses
{
    public sealed class RestauranteResponse
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "El nombre del restaurante es requerido")]
        [StringLength(255, MinimumLength = 3)]
        public string? Nombre { get; set; }

        [Required, StringLength(255, MinimumLength = 5)]
        public string? Direccion { get; set; }

        [Required, Phone]
        public string? Telefono { get; set; }

        [Required, EmailAddress]
        public string? Correo { get; set; }

        public TimeOnly HorarioApertura { get; set; }
        public TimeOnly HorarioCierre { get; set; }
    }
}