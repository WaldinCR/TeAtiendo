using System;
using System.Collections.Generic;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Segurity;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Orden : BaseEntity
    {
        public Guid UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public EstadoOrden Estado { get; set; }

        // Propiedades de Navegación
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Pago? Pago { get; set; }
        public virtual ICollection<OrdenDetalle> Detalles { get; set; } = new List<OrdenDetalle>();
    }

    public enum EstadoOrden
    {
        Pendiente = 1,
        EnPreparacion = 2,
        Listo = 3,
        Entregado = 4,
        Cancelado = 5
    }
}