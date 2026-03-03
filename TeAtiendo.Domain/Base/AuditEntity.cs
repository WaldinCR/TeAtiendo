using System;
using System.Text;
using System.Threading.Tasks;


namespace TeAtiendo.Domain.Base
{
    public abstract class AuditEntity
    {
        public string? Actor { get; set; }
        public string? Operacion { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        public bool Activo { get; set; } = true;
    }
}