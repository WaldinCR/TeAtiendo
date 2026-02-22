using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Entities.Social;

namespace TeAtiendo.Domain.Entities.Catalog
{
    internal class Restaurante
    {
        public int IdRestaurante { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;

        public string Estado { get; set; } = "Activo";

        public TimeOnly HorarioApertura { get; set; }
        public TimeOnly HorarioCierre { get; set; }

        // Navegación
        public ICollection<Menu> Menus { get; set; } = new List<Menu>();
        public ICollection<Mesa> Mesas { get; set; } = new List<Mesa>();
        public ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
        public ICollection<Resena> Resenas { get; set; } = new List<Resena>();
    }
}
