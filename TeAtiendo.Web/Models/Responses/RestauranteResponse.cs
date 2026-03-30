namespace TeAtiendo.Web.Models.Reponses
{
    public class RestauranteResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string HorarioApertura { get; set; } = string.Empty;
        public string HorarioCierre { get; set; } = string.Empty;
    }

    public class RestauranteApiResponse
    {
        public string Message { get; set; } = string.Empty;
        public List<RestauranteResponse> Data { get; set; } = new();
    }

    public class RestauranteApiSingleResponse
    {
        public string Message { get; set; } = string.Empty;
        public RestauranteResponse? Data { get; set; }
    }
}