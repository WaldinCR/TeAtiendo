using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Catalog;

namespace TeAtiendo.Domain.Entities.Catalog
{
    public class Menu : BaseEntity
    {
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public Guid RestauranteId { get; set; }

        // Relación
        public virtual Restaurante? Restaurante { get; set; }
        public virtual ICollection<CategoriaPlato> Categorias { get; set; } = new List<CategoriaPlato>();
    }
}