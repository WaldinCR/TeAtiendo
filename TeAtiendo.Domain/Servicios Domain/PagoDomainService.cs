using System;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Excepciones;

namespace TeAtiendo.Domain.Servicios_Domain
{
    public class PagoDomainService
    {
        public void ValidarPago(Pago pago)
        {
            if (pago == null)
            {
                throw new TeAtiendoExcepcion("El pago no puede ser nulo.");
            }

            if (pago.Monto <= 0)
            {
                throw new TeAtiendoExcepcion("El monto del pago debe ser mayor que cero.");
            }
        }
    }
}