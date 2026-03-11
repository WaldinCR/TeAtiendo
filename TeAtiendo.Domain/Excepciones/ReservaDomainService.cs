using System;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Excepciones;

namespace TeAtiendo.Domain.ServiciosDomain
{
    public class ReservaDomainService
    {
        public void ValidarHorarioReserva(Reserva reserva)
        {
            if (reserva.FechaReserva < DateTime.Now)
            {
                throw new TeAtiendoException("No se puede realizar una reserva para una fecha pasada.");
            }

            if (reserva.CantidadPersonas <= 0)
            {
                throw new TeAtiendoException("La reserva debe ser para al menos 1 persona.");
            }
        }
    }
}