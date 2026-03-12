using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Disponibilidad : BaseEntity
    {
        public Guid RestauranteId { get; set; }

        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        public EstadoDisponibilidad Estado { get; set; } = EstadoDisponibilidad.Libre;

        public virtual Restaurante Restaurante { get; set; } = null!;
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();
    }
}