namespace TeAtiendo.Web.Models.Reponses
{
    public class MenuResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public Guid RestauranteId { get; set; }
        public List<PlatoResponse>? Platos { get; set; }
    }
    public class PlatoResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public decimal Precio { get; set; }
        public Guid MenuId { get; set; }
        public Guid CategoriaPlatoId { get; set; }
    }
}
