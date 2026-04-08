using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Requests;

public class RegisterRequest
{
    [JsonPropertyName("nombre")]
    public string Nombre { get; set; } = "";

    [JsonPropertyName("correo")]
    public string Correo { get; set; } = "";

    [JsonPropertyName("password")]
    public string Password { get; set; } = "";
}