using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Segurity;

namespace TeAtiendo.Domain.Entities.Admin
{
    public class ModeracionContenido : BaseEntity
    {
        public Guid AdminId { get; set; }
        public string TipoContenido { get; set; } = string.Empty;
        public Guid ContenidoId { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Estado { get; set; } = "pendiente";
        public DateTime Fecha { get; set; } = DateTime.UtcNow;

        public virtual Usuario Admin { get; set; } = null!;
    }
}