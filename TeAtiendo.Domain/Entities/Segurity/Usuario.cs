using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Operations;
using System;       
using TeAtiendo.Domain.Entities.Social;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Segurity
{
    public class Usuario : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public RolUsuario Rol { get; set; }

        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
        public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
        public virtual ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
    }
}