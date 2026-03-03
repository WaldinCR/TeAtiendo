using System;
using System.Collections.Generic;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Entities.Social;

namespace TeAtiendo.Domain.Entities.Catalog
{
    public class Restaurante : BaseEntity
    {
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public TimeOnly HorarioApertura { get; set; }
        public TimeOnly HorarioCierre { get; set; }

        public virtual ICollection<Menu> Menus { get; set; } = new List<Menu>();
        public virtual ICollection<Mesa> Mesas { get; set; } = new List<Mesa>();
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
        public virtual ICollection<Orden> Ordenes { get; set; } = new List<Orden>();
        public virtual ICollection<Resena> Resenas { get; set; } = new List<Resena>();
    }
}