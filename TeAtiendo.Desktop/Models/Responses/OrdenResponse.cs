using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Desktop.Models.Responses
{
    public sealed class OrdenResponse
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid RestauranteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public EstadoOrden Estado { get; set; }
        public string TipoOrden { get; set; } = "anticipada";
    }
}