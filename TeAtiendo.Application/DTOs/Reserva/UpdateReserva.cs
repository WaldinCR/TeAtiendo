using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.DTOs.Reserva
{
    public sealed class UpdateReservaDto
    {
        public EstadoReserva EstadoReserva { get; set; }
        public int CantidadPersonas { get; set; }
    }
}