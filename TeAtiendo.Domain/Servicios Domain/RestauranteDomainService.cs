using System;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Excepciones;

namespace TeAtiendo.Domain.ServiciosDomain
{
    public class RestauranteDomainService
    {
        public void ValidarRestaurante(Restaurante restaurante)
        {
            if (restaurante == null)
            {
                throw new TeAtiendoExcepcion("El restaurante no puede ser nulo.");
            }

            if (string.IsNullOrWhiteSpace(restaurante.Nombre))
            {
                throw new TeAtiendoExcepcion("El restaurante debe tener un nombre.");
            }
        }
    }
}