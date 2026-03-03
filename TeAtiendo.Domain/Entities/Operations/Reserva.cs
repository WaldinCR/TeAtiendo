using System;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Reserva : BaseEntity
    {
        public int IdUsuario { get; set; }
        public int IdRestaurante { get; set; }
        public int IdMesa { get; set; }
        public int IdDisponibilidad { get; set; }

        public DateTime Fecha { get; set; }
        public TimeOnly Hora { get; set; }
        public int CantidadPersonas { get; set; }

        public EstadoReserva Estado { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Restaurante Restaurante { get; set; } = null!;
        public virtual Mesa Mesa { get; set; } = null!;
        public virtual Disponibilidad Disponibilidad { get; set; } = null!;
    }
}