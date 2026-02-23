using System;
using System.Collections.Generic;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Entities.Social;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Segurity
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;

        public RolUsuario Rol { get; set; }

        public string Estado { get; set; } = "Activo";
        public DateTime FechaRegistro { get; set; }

        // Navegación
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
        public ICollection<Resena> Resenas { get; set; } = new List<Resena>();
        public ICollection<Notificacion> Notificaciones { get; set; } = new List<Notificacion>();
    }
}