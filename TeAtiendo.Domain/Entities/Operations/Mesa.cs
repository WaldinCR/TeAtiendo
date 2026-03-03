using System;
using System.Collections.Generic;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Catalog;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Mesa : BaseEntity
    {
        public Guid RestauranteId { get; set; }

        public int Numero { get; set; }
        public int Capacidad { get; set; }

        public virtual Restaurante Restaurante { get; set; } = null!;
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}