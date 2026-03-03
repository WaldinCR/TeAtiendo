using System;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Segurity;

namespace TeAtiendo.Domain.Entities.Auditory
{
    public class Auditoria : BaseEntity
    {
        public int IdAdmin { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string Modulo { get; set; } = string.Empty;
        public string Detalle { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;

        public virtual Usuario Admin { get; set; } = null!;
    }
}