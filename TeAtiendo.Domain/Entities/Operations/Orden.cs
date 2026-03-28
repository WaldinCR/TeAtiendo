using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Orden : BaseEntity
    {
        public Guid UsuarioId { get; set; }
        public Guid RestauranteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public EstadoOrden Estado { get; set; } = EstadoOrden.Pendiente;
        public string TipoOrden { get; set; } = "anticipada";

        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Restaurante Restaurante { get; set; } = null!;
        public virtual Pago? Pago { get; set; }
        public virtual ICollection<OrdenDetalle> Detalles { get; set; } = new List<OrdenDetalle>();
    }
}