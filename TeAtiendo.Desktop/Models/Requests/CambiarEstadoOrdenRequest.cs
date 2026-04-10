using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Desktop.Models.Requests
{
    public sealed class CambiarEstadoOrdenRequest
    {
        public EstadoOrden NuevoEstado { get; set; }
    }
}