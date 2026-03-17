namespace TeAtiendo.Application.DTOs.Menu
{
    public class CreateMenuDto
    {
        public required string Nombre { get; set; }
        public string? Descripcion { get; set; }
        public Guid RestauranteId { get; set; }
    }
}