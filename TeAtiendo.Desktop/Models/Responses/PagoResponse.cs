using TeAtiendo.Application.DTOs.Pago;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Desktop.Models.Responses
{
    public sealed class PagoResponse
    {
        public Guid Id { get; set; }
        public Guid OrdenId { get; set; }
        public decimal Monto { get; set; }
        public EstadoPago EstadoPago { get; set; }
        public MetodoPago MetodoPago { get; set; }
    }
}