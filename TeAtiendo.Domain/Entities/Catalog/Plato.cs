using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeAtiendo.Domain.Entities.Catalog
{
    internal class Plato
    {
        public int IdPlato { get; set; }

        public int IdCategoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public decimal Precio { get; set; }
        public bool Disponible { get; set; }

        // Navegación
        public CategoriaPlato CategoriaPlato { get; set; } = null!;
    }
}
