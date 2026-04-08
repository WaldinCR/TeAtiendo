using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Responses;

public class MenuResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("restauranteId")]
    public int RestauranteId { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = "";

    [JsonPropertyName("descripcion")]
    public string? Descripcion { get; set; }

    [JsonPropertyName("activo")]
    public bool Activo { get; set; } = true;
}