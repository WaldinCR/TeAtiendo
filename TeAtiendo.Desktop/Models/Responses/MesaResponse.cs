using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Responses;

public class MesaResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("restauranteId")]
    public int RestauranteId { get; set; }

    [JsonPropertyName("numero")]
    public int Numero { get; set; }

    [JsonPropertyName("capacidad")]
    public int Capacidad { get; set; }

    [JsonPropertyName("activa")]
    public bool Activa { get; set; } = true;
}