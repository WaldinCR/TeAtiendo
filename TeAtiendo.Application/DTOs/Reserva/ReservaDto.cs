using System;
using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Application.DTOs.Reserva
{
    public class ReservaDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid RestauranteId { get; set; }
        public Guid MesaId { get; set; }
        public Guid DisponibilidadId { get; set; }
        public DateTime FechaReserva { get; set; }
        public int CantidadPersonas { get; set; }
        public EstadoReserva EstadoReserva { get; set; }
    }
}