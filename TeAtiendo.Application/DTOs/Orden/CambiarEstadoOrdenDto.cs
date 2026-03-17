using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.DTOs.Orden
{
    public sealed class CambiarEstadoOrdenDto
    {
        public EstadoOrden NuevoEstado { get; set; }
    }
}