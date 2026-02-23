using System;
using System.Collections.Generic;
using TeAtiendo.Domain.Entities.Catalog;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Mesa
    {
        public int IdMesa { get; set; }

        public int IdRestaurante { get; set; }
        public int Numero { get; set; }
        public int Capacidad { get; set; }

        public string Estado { get; set; } = "Activa";

        // Navegación
        public Restaurante Restaurante { get; set; } = null!;
        public ICollection<Disponibilidad> Disponibilidades { get; set; } = new List<Disponibilidad>();
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}