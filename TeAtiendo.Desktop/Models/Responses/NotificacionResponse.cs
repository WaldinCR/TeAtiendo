using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Responses;

public class NotificacionResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("usuarioId")]
    public int UsuarioId { get; set; }

    [JsonPropertyName("titulo")]
    public string Titulo { get; set; } = "";

    [JsonPropertyName("mensaje")]
    public string Mensaje { get; set; } = "";

    [JsonPropertyName("leida")]
    public bool Leida { get; set; }

    [JsonPropertyName("fecha")]
    public DateTime Fecha { get; set; }
}