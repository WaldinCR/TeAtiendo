using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Pago : BaseEntity
    {
        public Guid OrdenId { get; set; }

        public decimal Monto { get; set; }
        public EstadoPago EstadoPago { get; set; } = EstadoPago.Pendiente;

        public MetodoPago MetodoPago { get; set; }

        public virtual Orden Orden { get; set; } = null!;
    }
}