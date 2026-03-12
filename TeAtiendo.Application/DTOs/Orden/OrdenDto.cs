using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.DTOs.Orden
{
    public sealed class OrdenDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public EstadoOrden Estado { get; set; }

        public List<OrdenDetalleDto> Detalles { get; set; } = new();
    }
}