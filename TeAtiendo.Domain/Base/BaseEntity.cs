using System;

namespace TeAtiendo.Domain.Base
{
    public abstract class BaseEntity: AuditEntity   
    {
        public Guid Id { get; set; }

        // AUDITORÍA
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public string? UsuarioCreacion { get; set; }
        public string? UsuarioModificacion { get; set; }

        //SOFT DELETE
        public bool Activo { get; set; } = true;

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            FechaCreacion = DateTime.UtcNow;
            Timestamp = DateTime.UtcNow;
        }
    }
}