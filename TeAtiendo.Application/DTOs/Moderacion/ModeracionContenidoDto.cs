namespace TeAtiendo.Application.DTOs.Moderacion
{
    public sealed class ModeracionContenidoDto
    {
        public Guid Id { get; set; }
        public Guid AdminId { get; set; }
        public string TipoContenido { get; set; } = string.Empty;
        public Guid ContenidoId { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Estado { get; set; } = "pendiente";
        public DateTime Fecha { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}