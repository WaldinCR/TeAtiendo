namespace TeAtiendo.Application.DTOs
{
    public class CategoriaplatoDto
    {
        public Guid Id { get; set; }
        public Guid MenuId { get; set; }

        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }
    }
}