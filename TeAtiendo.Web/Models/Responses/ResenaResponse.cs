namespace TeAtiendo.Web.Models.Reponses
{
    public class ResenaResponse
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid RestauranteId { get; set; }
        public Guid? ReservaId { get; set; }
        public Guid? OrdenId { get; set; }
        public int Calificacion { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public int Estado { get; set; }
    }
}