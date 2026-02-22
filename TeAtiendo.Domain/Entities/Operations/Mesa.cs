using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Base;

namespace TeAtiendo.Domain.Entities.Operations
{
    internal class Mesa
    {
        public int IdMesa { get; set; } // PK

        public int IdRestaurante { get; set; } // FK
        public int Numero { get; set; }
        public int Capacidad { get; set; }

        public string Estado { get; set; } = "Activa";

        // Navegación
        public Restaurante Restaurante { get; set; } = null!;
        public ICollection<Disponibilidad> Disponibilidades { get; set; } = new List<Disponibilidad>();
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}
