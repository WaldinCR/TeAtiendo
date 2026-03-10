using System;
using System.Collections.Generic;
using TeAtiendo.Domain.Base;    
using TeAtiendo.Domain.Enums;
using TeAtiendo.Domain.Entities.Catalog;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Disponibilidad: BaseEntity 
    {
        public Guid RestauranteId { get; set; }

        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        public virtual Restaurante Restaurante { get; set; } = null!;
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}