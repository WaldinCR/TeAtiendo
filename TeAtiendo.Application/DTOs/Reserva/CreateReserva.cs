namespace TeAtiendo.Application.DTOs.Reserva
{
    public sealed class CreateReservaDto
    {
        public Guid UsuarioId { get; set; }
        public Guid RestauranteId { get; set; }
        public Guid MesaId { get; set; }
        public Guid DisponibilidadId { get; set; }

        public DateTime FechaReserva { get; set; }
        public int CantidadPersonas { get; set; }
    }
}