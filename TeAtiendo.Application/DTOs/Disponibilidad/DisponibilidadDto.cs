using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.DTOs
{
    public class DisponibilidadDto
    {
        public Guid Id { get; set; }
        public Guid RestauranteId { get; set; }

        public DateTime Fecha { get; set; }
        public TimeSpan HoraInicio { get; set; }
        public TimeSpan HoraFin { get; set; }

        public EstadoDisponibilidad Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}