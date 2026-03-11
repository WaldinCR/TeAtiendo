using TeAtiendo.Domain.Base;

namespace TeAtiendo.Domain.Entities.Catalog
{
    public class CategoriaPlato : BaseEntity
    {
        public Guid MenuId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public virtual Menu Menu { get; set; } = null!;
        public virtual ICollection<Plato> Platos { get; set; } = new List<Plato>();
    }
}