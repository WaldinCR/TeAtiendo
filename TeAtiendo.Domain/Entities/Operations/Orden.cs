using System;
using System.Collections.Generic;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Operations
{
    internal class Orden
    {
        public int? IdReserva { get; set; }

        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        public EstadoOrden Estado { get; set; }

        // E-R: tipoOrden string 'anticipada'
        public string TipoOrden { get; set; } = "anticipada";

        // Navegación
        public Usuario Usuario { get; set; } = null!;
        public Restaurante Restaurante { get; set; } = null!;
        public Reserva? Reserva { get; set; }

        public ICollection<OrdenDetalle> Detalles { get; set; } = new List<OrdenDetalle>();
        public ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }

    
}
