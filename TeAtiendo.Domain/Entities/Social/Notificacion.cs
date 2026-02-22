using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Social
{
    internal class Notificacion
    {
        public int IdNotificacion { get; set; } // PK

        public int IdUsuario { get; set; }

        public string Tipo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;

        public DateTime FechaEnvio { get; set; }
        public bool Leida { get; set; }

        public string Canal { get; set; } = "email";

        // Navegación
        public Usuario Usuario { get; set; } = null!;
    }
}
