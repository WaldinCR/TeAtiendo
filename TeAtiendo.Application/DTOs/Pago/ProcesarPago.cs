using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.DTOs.Pago
{
    public sealed class ProcesarPagoDto
    {
        public Guid OrdenId { get; set; }
        public MetodoPago MetodoPago { get; set; }
    }
}