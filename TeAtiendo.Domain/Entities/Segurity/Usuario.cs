using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Entities.Social;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Segurity
{
    public class Usuario : BaseEntity
    {
        [Required(ErrorMessage = "El nombre completo es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 30 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "El formato del correo electrónico no es válido")]
        [StringLength(150, ErrorMessage = "El correo no puede exceder los 150 caracteres")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [StringLength(255, MinimumLength = 8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string PasswordHash { get; set; } = string.Empty;

        [Required]
        public RolUsuario Rol { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
        public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
        public virtual ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
    }
}