namespace TeAtiendo.Application.DTOs
{
    public class PlatoDto
    {
        public Guid Id { get; set; }
        public Guid CategoriaPlatoId { get; set; }
        public Guid MenuId { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public bool Disponible { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool Activo { get; set; }
    }
}