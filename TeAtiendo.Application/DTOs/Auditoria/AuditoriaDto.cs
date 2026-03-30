namespace TeAtiendo.Application.DTOs.Auditoria
{
    public sealed class AuditoriaDto
    {
        public Guid Id { get; set; }
        public Guid AdminId { get; set; }
        public DateTime Fecha { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string Modulo { get; set; } = string.Empty;
        public string Detalle { get; set; } = string.Empty;
        public string Ip { get; set; } = string.Empty;
        public DateTime FechaCreacion { get; set; }
    }
}