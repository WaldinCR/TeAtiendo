using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.DTOs.Orden
{
    public sealed class UpdateOrdenDto
    {
        public EstadoOrden Estado { get; set; }
    }
}