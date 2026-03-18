using System.ComponentModel.DataAnnotations;

namespace TeAtiendo.Application.DTOs.Restaurante
{
    public sealed class UpdateRestauranteDto
    {
        [Required(ErrorMessage = "El nombre del restaurante es requerido")]
        [StringLength(255, MinimumLength = 3)]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "La dirección es requerida")]
        [StringLength(255, MinimumLength = 5)]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es requerido")]
        [Phone]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El correo es requerido")]
        [EmailAddress]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "El horario de apertura es requerido")]
        public TimeOnly HorarioApertura { get; set; }

        [Required(ErrorMessage = "El horario de cierre es requerido")]
        public TimeOnly HorarioCierre { get; set; }
    }
}