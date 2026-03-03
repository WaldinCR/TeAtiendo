using System;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Reserva
    {
        public int IdReserva { get; set; }

        public int IdUsuario { get; set; }
        public int IdRestaurante { get; set; }
        public int IdMesa { get; set; }
        public int IdDisponibilidad { get; set; }

        public DateTime Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public int CantidadPersonas { get; set; }

        public EstadoReserva Estado { get; set; }

        // Navegación
        public Usuario Usuario { get; set; } = null!;
        public Restaurante Restaurante { get; set; } = null!;
        public Mesa Mesa { get; set; } = null!;
        public Disponibilidad Disponibilidad { get; set; } = null!;
    }
}