using System;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Domain.Enums;
using TeAtiendo.Domain.Base;    
namespace TeAtiendo.Domain.Entities.Social
{
    public class Resena : BaseEntity
    {
        public Guid UsuarioId { get; set; }
        public Guid RestauranteId { get; set; }

        public Guid? ReservaId { get; set; }
        public Guid? OrdenId { get; set; }

        public int Calificacion { get; set; }
        public string Comentario { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }= DateTime.UtcNow;   
        public EstadoResena Estado { get; set; }

        // Navegación
        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Restaurante Restaurante { get; set; } = null!;
        public virtual Reserva? Reserva { get; set; }
        public virtual Orden? Orden { get; set; }
    }
}