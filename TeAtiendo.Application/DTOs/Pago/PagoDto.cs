using System;
using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Application.DTOs.Pago
{
    public class PagoDto
    {
        public Guid Id { get; set; }
        public Guid OrdenId { get; set; }
        public decimal Monto { get; set; }
        public EstadoPago EstadoPago { get; set; }
        public MetodoPago MetodoPago { get; set; }
    }
}