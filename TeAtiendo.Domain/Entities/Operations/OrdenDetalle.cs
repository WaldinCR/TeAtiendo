using System;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Catalog;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class OrdenDetalle : BaseEntity
    {
        public Guid OrdenId { get; set; }
        public Guid PlatoId { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        public virtual Orden Orden { get; set; } = null!;
        public virtual Plato Plato { get; set; } = null!;
    }
}