using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeAtiendo.Domain.Entities.Catalog
{
    internal class CategoriaPlato
    {
        public int IdCategoria { get; set; }

        public int IdMenu { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        // Navegación
        public Menu Menu { get; set; } = null!;
        public ICollection<Plato> Platos { get; set; } = new List<Plato>();
    }
}
