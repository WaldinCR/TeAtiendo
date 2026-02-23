using System;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Disponibilidad
    {
        public int IdDisponibilidad { get; set; }

        public int IdMesa { get; set; }
        public DateTime Fecha { get; set; }
        public TimeOnly Hora { get; set; }

        public EstadoDisponibilidad Estado { get; set; }

        // Navegación
        public Mesa Mesa { get; set; } = null!;
    }
}