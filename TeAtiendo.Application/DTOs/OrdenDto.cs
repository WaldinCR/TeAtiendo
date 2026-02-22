using System;
using System.Collections.Generic;

namespace TeAtiendo.Application.DTOs
{
    public class OrdenDto
    {
        public int IdOrden { get; set; }
        public int IdUsuario { get; set; }
        public int IdRestaurante { get; set; }

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

        // Pendiente / Confirmada / Preparando / Entregada / Cancelada (ajustar según SAD)
        public string Estado { get; set; } = "Pendiente";

        public decimal Subtotal { get; set; }
        public decimal Total { get; set; }

        public List<OrdenDetalleDto> Detalles { get; set; } = new List<OrdenDetalleDto>();
    }

    public class OrdenDetalleDto
    {
        public int IdPlato { get; set; }
        public string NombrePlato { get; set; } = string.Empty;
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal TotalLinea { get; set; }
    }

    public class OrdenCreateRequest
    {
        public int IdUsuario { get; set; }
        public int IdRestaurante { get; set; }
        public List<OrdenDetalleCreateRequest> Detalles { get; set; } = new List<OrdenDetalleCreateRequest>();
    }

    public class OrdenDetalleCreateRequest
    {
        public int IdPlato { get; set; }
        public int Cantidad { get; set; }
    }
}
