using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Responses;

public class UsuarioResponse
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = "";

    [JsonPropertyName("correo")]
    public string Correo { get; set; } = "";

    [JsonPropertyName("rol")]
    public int Rol { get; set; }

    [JsonPropertyName("activo")]
    public bool Activo { get; set; } = true;
}