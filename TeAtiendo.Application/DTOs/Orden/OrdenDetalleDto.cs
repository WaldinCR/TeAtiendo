namespace TeAtiendo.Application.DTOs.Orden
{
    public sealed class OrdenDetalleDto
    {
        public Guid PlatoId { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}