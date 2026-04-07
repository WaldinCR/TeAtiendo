namespace TeAtiendo.Web.Models.Reponses
{
    public class PagoResponse
    {
        public Guid Id { get; set; }
        public Guid OrdenId { get; set; }
        public decimal Monto { get; set; }
        public int EstadoPago { get; set; }
        public int MetodoPago { get; set; }
        public DateTime FechaPago { get; set; }
    }
}
