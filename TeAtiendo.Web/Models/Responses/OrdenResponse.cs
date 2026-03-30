namespace TeAtiendo.Web.Models.Reponses
{
    public class OrdenResponse
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid RestauranteId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public int Estado { get; set; }
        public string TipoOrden { get; set; } = string.Empty;
        public List<OrdenDetalleResponse> Detalles { get; set; } = new();
    }

    public class OrdenDetalleResponse
    {
        public Guid PlatoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}