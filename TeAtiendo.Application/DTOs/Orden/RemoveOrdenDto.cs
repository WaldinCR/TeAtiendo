namespace TeAtiendo.Application.DTOs.Orden
{
    public sealed class RemoveOrdenDto
    {
        public Guid OrdenId { get; set; }
        public Guid UserId { get; set; }
    }
}