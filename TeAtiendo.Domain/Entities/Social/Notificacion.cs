using System;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Segurity;

namespace TeAtiendo.Domain.Entities.Social
{
    public class Notificacion : BaseEntity
    {
        public Guid UsuarioId { get; set; }

    
        public string Tipo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;

        public DateTime FechaEnvio { get; set; }= DateTime.UtcNow;  
        public bool Leida { get; set; }

        public string Canal { get; set; } = "email";

        // Navegación
        public virtual Usuario Usuario { get; set; } = null!;
    }
}