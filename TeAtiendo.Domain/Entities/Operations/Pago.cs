using System;
using System.Collections.Generic;
using TeAtiendo.Domain.Base;    
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Pago: BaseEntity   
    {
        public int IdPago { get; set; }

        public int IdOrden { get; set; }
        public decimal Monto { get; set; }

        public string MetodoPago { get; set; } = "Stripe";

        public EstadoPago EstadoPago { get; set; }

        public DateTime FechaPago { get; set; }

        // referenciaExterna (Stripe paymentIntent/charge id)
        public string? ReferenciaExterna { get; set; }

        // Navegación
        public Orden Orden { get; set; } = null!;
    }
}