using System;
using System.Collections.Generic;
using System.Linq;
using TeAtiendo.Application.DTOs;

namespace TeAtiendo.Application.Services
{
    public class PagoService
    {
        private static readonly List<PagoDto> _pagos = new List<PagoDto>();
        private static int _id = 1;

        public PagoDto ProcesarPago(PagoCreateRequest request)
        {
            var pago = new PagoDto
            {
                IdPago = _id++,
                IdOrden = request.IdOrden,
                Monto = request.Monto,
                Metodo = request.Metodo,
                FechaPago = DateTime.UtcNow,
                Estado = "Pagado" // Simulado
            };

            _pagos.Add(pago);
            return pago;
        }

        public List<PagoDto> ObtenerPagosPorOrden(int idOrden)
        {
            return _pagos.Where(x => x.IdOrden == idOrden).ToList();
        }
    }
}
