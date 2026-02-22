using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Segurity;

namespace TeAtiendo.Domain.Entities.Auditory
{
    internal class Auditoria
    {
        public int IdLog { get; set; }

        public int IdAdmin { get; set; }
        public DateTime Fecha { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string Modulo { get; set; } = string.Empty;
        public string Detalle { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;

        // Navegación (Admin )
        public Usuario Admin { get; set; } = null!;
    }
}
