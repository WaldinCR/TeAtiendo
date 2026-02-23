using System;
using TeAtiendo.Domain.Entities.Catalog;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class OrdenDetalle
    {
        public int IdDetalle { get; set; }

        public int IdOrden { get; set; }
        public int IdPlato { get; set; }

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }

        // Navegación
        public Orden Orden { get; set; } = null!;
        public Plato Plato { get; set; } = null!;
    }
}