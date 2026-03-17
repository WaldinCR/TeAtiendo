namespace TeAtiendo.Application.DTOs.Menu
{
    public class UpdateMenuDto
    {
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public Guid? RestauranteId { get; set; }
    }
}