namespace TeAtiendo.Web.Models.Reponses
{
    public class MesaResponse
    {
        public Guid Id { get; set; }
        public Guid RestauranteId { get; set; }
        public int NumeroMesa { get; set; }
        public int Capacidad { get; set; }
        public bool Disponible { get; set; }
    }
}