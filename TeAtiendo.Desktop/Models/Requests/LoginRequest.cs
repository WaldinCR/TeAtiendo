using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Requests;

public class LoginRequest
{
    [JsonPropertyName("correo")]
    public string Correo { get; set; } = "";

    [JsonPropertyName("password")]
    public string Password { get; set; } = "";
}