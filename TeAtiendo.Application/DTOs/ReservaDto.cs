using System;

namespace TeAtiendo.Application.DTOs
{
    public class ReservaDto
    {
        public int IdReserva { get; set; }
        public int IdUsuario { get; set; }
        public int IdRestaurante { get; set; }
        public int IdMesa { get; set; }

        public DateTime Fecha { get; set; }
        public string Hora { get; set; } = "19:00";
        public int CantidadPersonas { get; set; }

        // Pendiente / Confirmada / Rechazada / Cancelada (ajustar según SAD)
        public string Estado { get; set; } = "Pendiente";
    }

    public class ReservaCreateRequest
    {
        public int IdUsuario { get; set; }
        public int IdRestaurante { get; set; }
        public int IdMesa { get; set; }
        public DateTime Fecha { get; set; }
        public string Hora { get; set; } = "19:00";
        public int CantidadPersonas { get; set; }
    }
}
