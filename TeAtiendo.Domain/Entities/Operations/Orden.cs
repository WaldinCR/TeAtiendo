using System;
using System.Collections.Generic;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Entities.Segurity;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Orden : BaseEntity
    {
        public Guid UsuarioId { get; set; }
        public Guid RestauranteId { get; set; }

        public EstadoOrden EstadoOrden { get; set; }
        public decimal Total { get; set; }

        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Restaurante Restaurante { get; set; } = null!;
        public virtual ICollection<OrdenDetalle> OrdenDetalles { get; set; } = new List<OrdenDetalle>();
        public virtual ICollection<Pago> Pagos { get; set; } = new List<Pago>();
    }

    public enum EstadoOrden
    {
        Pendiente = 1,
        EnProceso = 2,
        Completada = 3,
        Cancelada = 4
    }
}