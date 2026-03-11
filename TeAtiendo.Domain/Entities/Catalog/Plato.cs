using TeAtiendo.Domain.Base;

namespace TeAtiendo.Domain.Entities.Catalog
{
    public class Plato : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public decimal Precio { get; set; }

        public Guid MenuId { get; set; }
        public virtual Menu Menu { get; set; } = null!;

        public Guid CategoriaPlatoId { get; set; }
        public virtual CategoriaPlato CategoriaPlato { get; set; } = null!;
    }
}