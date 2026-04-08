using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Responses;

public class DisponibilidadResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("restauranteId")]
    public int RestauranteId { get; set; }

    [JsonPropertyName("mesaId")]
    public int MesaId { get; set; }

    [JsonPropertyName("fecha")]
    public DateTime Fecha { get; set; }

    [JsonPropertyName("disponible")]
    public bool Disponible { get; set; }
}