using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Responses;

public class PagoResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("ordenId")]
    public int OrdenId { get; set; }

    [JsonPropertyName("monto")]
    public decimal Monto { get; set; }

    [JsonPropertyName("fecha")]
    public DateTime Fecha { get; set; }

    [JsonPropertyName("estado")]
    public int Estado { get; set; }

    [JsonPropertyName("metodo")]
    public string? Metodo { get; set; }
}