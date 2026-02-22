using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Base;


namespace TeAtiendo.Domain.Entities.Operations
{
    internal class OrdenDetalle
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
