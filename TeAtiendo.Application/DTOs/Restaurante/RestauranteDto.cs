using System.ComponentModel.DataAnnotations;


      namespace TeAtiendo.Application.DTOs.Restaurante
    {
        public sealed class RestauranteDto
        {
            public Guid Id { get; set; }

            [Required(ErrorMessage = "El nombre del restaurante es requerido")]
            [StringLength(255, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 255 caracteres")]
            public string? Nombre { get; set; }

            [Required(ErrorMessage = "La dirección es requerida")]
            [StringLength(255, MinimumLength = 5, ErrorMessage = "La dirección debe tener entre 5 y 255 caracteres")]
            public string? Direccion { get; set; }

            [Required(ErrorMessage = "El teléfono es requerido")]
            [Phone(ErrorMessage = "El formato del teléfono no es válido")]
            public string? Telefono { get; set; }

            [Required(ErrorMessage = "El correo es requerido")]
            [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
            public string? Correo { get; set; }

            [Required(ErrorMessage = "El horario de apertura es requerido")]
            public TimeOnly HorarioApertura { get; set; }

            [Required(ErrorMessage = "El horario de cierre es requerido")]
            public TimeOnly HorarioCierre { get; set; }
        }
    }
 