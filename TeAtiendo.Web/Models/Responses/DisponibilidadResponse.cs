namespace TeAtiendo.Web.Models.Reponses
{
    public class DisponibilidadResponse
    {
        public Guid Id { get; set; }
        public Guid RestauranteId { get; set; }
        public DateTime Fecha { get; set; }
        public string HoraInicio { get; set; } = string.Empty;
        public string HoraFin { get; set; } = string.Empty;
        public int Estado { get; set; }
    }
}