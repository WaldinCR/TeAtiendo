using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.DTOs
{
    public class ResenaDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid RestauranteId { get; set; }

        public Guid? ReservaId { get; set; }
        public Guid? OrdenId { get; set; }

        public int Calificacion { get; set; }
        public string Comentario { get; set; } = string.Empty;

        public DateTime Fecha { get; set; }
        public EstadoResena Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}