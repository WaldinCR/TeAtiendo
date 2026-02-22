using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeAtiendo.Domain.Base
{
    internal class AuditEntity
    {
        public string Actor { get; set; } = string.Empty;
        public string Operacion { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
