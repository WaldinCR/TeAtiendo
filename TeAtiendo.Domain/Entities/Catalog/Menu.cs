using System;
using System.Collections.Generic;
using TeAtiendo.Domain.Base;

namespace TeAtiendo.Domain.Entities.Catalog
{
    public class Menu : BaseEntity
    {
        public Guid RestauranteId { get; set; }
        public string Nombre { get; set; } = string.Empty;

        public virtual Restaurante Restaurante { get; set; } = null!;
        public virtual ICollection<CategoriaPlato> Categorias { get; set; } = new List<CategoriaPlato>();
    }
}