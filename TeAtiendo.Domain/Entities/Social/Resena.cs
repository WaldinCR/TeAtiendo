using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Entities.Segurity;

namespace TeAtiendo.Domain.Entities.Social
{
    internal class Resena
    {
        public int IdResena { get; set; }

        public int IdUsuario { get; set; }
        public int IdRestaurante { get; set; }

        //idReserva OR idOrden (segun regla)
        public int? IdReserva { get; set; }
        public int? IdOrden { get; set; }

        public int Calificacion { get; set; }
        public string Comentario { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }

        public EstadoResena Estado { get; set; }

        // Navegación
        public Usuario Usuario { get; set; } = null!;
        public Restaurante Restaurante { get; set; } = null!;
        public Reserva? Reserva { get; set; }
        public Orden? Orden { get; set; }
    }
}
