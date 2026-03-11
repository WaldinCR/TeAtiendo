using System.Linq;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Excepciones;

namespace TeAtiendo.Domain.ServiciosDomain
{
    public class OrdenDomainService
    {
        public void ValidarOrden(Orden orden)
        {
            if (orden.OrdenDetalles == null || !orden.OrdenDetalles.Any())
            {
                throw new TeAtiendoException("La orden debe tener al menos un detalle.");
            }

            var totalCalculado = orden.OrdenDetalles.Sum(d => d.Cantidad * d.PrecioUnitario);

            if (orden.Total != totalCalculado)
            {
                throw new TeAtiendoException("El total de la orden no coincide con los detalles.");
            }
        }
    }
}