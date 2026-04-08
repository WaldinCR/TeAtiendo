using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Responses;

public class OrdenResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("usuarioId")]
    public int UsuarioId { get; set; }

    [JsonPropertyName("restauranteId")]
    public int RestauranteId { get; set; }

    [JsonPropertyName("fecha")]
    public DateTime Fecha { get; set; }

    [JsonPropertyName("total")]
    public decimal Total { get; set; }

    [JsonPropertyName("estado")]
    public int Estado { get; set; }
}