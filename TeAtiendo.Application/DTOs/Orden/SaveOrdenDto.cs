namespace TeAtiendo.Application.DTOs.Orden
{
    public sealed class SaveOrdenDto
    {
        public Guid UsuarioId { get; set; }
        public Guid RestauranteId { get; set; }
        public List<OrdenDetalleDto> Detalles { get; set; } = new();
    }
}