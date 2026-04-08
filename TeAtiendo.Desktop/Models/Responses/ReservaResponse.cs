using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Responses;

public class ReservaResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("usuarioId")]
    public int UsuarioId { get; set; }

    [JsonPropertyName("restauranteId")]
    public int RestauranteId { get; set; }

    [JsonPropertyName("mesaId")]
    public int MesaId { get; set; }

    [JsonPropertyName("fecha")]
    public DateTime Fecha { get; set; }

    // si tu API manda string, lo cambiamos luego
    [JsonPropertyName("estado")]
    public int Estado { get; set; }
}