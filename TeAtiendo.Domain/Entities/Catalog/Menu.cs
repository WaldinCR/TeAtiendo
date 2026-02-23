using System;
using System.Collections.Generic;

namespace TeAtiendo.Domain.Entities.Catalog
{
    public class Menu
    {
        public int IdMenu { get; set; }

        public int IdRestaurante { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Estado { get; set; } = "Activo";
        public DateTime FechaActualizacion { get; set; }

        // Navegación
        public Restaurante Restaurante { get; set; } = null!;
        public ICollection<CategoriaPlato> Categorias { get; set; } = new List<CategoriaPlato>();
    }
}