using System;
using System.Collections.Generic;
using TeAtiendo.Domain.Base;    

namespace TeAtiendo.Domain.Entities.Catalog
{
    public class Plato: BaseEntity
    {
        public Guid CategoriaPlatoId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }

        public virtual CategoriaPlato CategoriaPlato { get; set; } = null!;
    }
}