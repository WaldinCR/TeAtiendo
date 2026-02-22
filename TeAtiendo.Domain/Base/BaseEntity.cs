using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeAtiendo.Domain.Base
{
    internal class BaseEntity
    {
        public Guid Id { get; set; }

        // Auditoría
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        // Soft Delete
        public bool Activo { get; set; } = true;

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            FechaCreacion = DateTime.UtcNow;
        }
    }
}
