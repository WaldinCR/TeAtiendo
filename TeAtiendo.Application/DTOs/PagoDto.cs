using System;

namespace TeAtiendo.Application.DTOs
{
    public class PagoDto
    {
        public int IdPago { get; set; }
        public int IdOrden { get; set; }
        public decimal Monto { get; set; }
        public DateTime FechaPago { get; set; } = DateTime.UtcNow;

        // Pendiente / Pagado / Rechazado (ajustar según SAD)
        public string Estado { get; set; } = "Pendiente";

        // En universitario: “Simulado”, luego puede ser “Stripe”
        public string Metodo { get; set; } = "Simulado";
    }

    public class PagoCreateRequest
    {
        public int IdOrden { get; set; }
        public decimal Monto { get; set; }
        public string Metodo { get; set; } = "Simulado";
    }
}
