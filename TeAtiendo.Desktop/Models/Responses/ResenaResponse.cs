using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Responses;

public class ResenaResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("usuarioId")]
    public int UsuarioId { get; set; }

    [JsonPropertyName("restauranteId")]
    public int RestauranteId { get; set; }

    [JsonPropertyName("rating")]
    public int Rating { get; set; }

    [JsonPropertyName("comentario")]
    public string? Comentario { get; set; }

    [JsonPropertyName("fecha")]
    public DateTime Fecha { get; set; }
}