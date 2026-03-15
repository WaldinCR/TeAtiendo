namespace TeAtiendo.Application.DTOs
{
    public class MesaDto
    {
        public Guid Id { get; set; }
        public Guid RestauranteId { get; set; }
        public int NumeroMesa { get; set; }
        public int Capacidad { get; set; }
        public bool Disponible { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}