using System;
using TeAtiendo.Domain.Base;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Pago : BaseEntity
    {
        public Guid OrdenId { get; set; }

        public decimal Monto { get; set; }
        public EstadoPago EstadoPago { get; set; }
        public MetodoPago MetodoPago { get; set; }

        public virtual Orden Orden { get; set; } = null!;
    }

    public enum EstadoPago
    {
        Pendiente = 1,
        Procesado = 2,
        Rechazado = 3
    }

    public enum MetodoPago
    {
        Efectivo = 1,
        TarjetaCredito = 2,
        TarjetaDebito = 3,
        Transferencia = 4
    }
}