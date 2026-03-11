using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Domain.Entities.Operations
{
    public class Reserva : BaseEntity
    {
        public Guid UsuarioId { get; set; }
        public Guid RestauranteId { get; set; }
        public Guid MesaId { get; set; }
        public Guid DisponibilidadId { get; set; }

        public DateTime FechaReserva { get; set; }
        public int CantidadPersonas { get; set; }
        public EstadoReserva EstadoReserva { get; set; } = EstadoReserva.Pendiente;

        public virtual Usuario Usuario { get; set; } = null!;
        public virtual Restaurante Restaurante { get; set; } = null!;
        public virtual Mesa Mesa { get; set; } = null!;
        public virtual Disponibilidad Disponibilidad { get; set; } = null!;
    }
}