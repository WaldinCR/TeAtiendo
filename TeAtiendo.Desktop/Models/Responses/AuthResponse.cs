using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Responses;

public class AuthResponse
{
    [JsonPropertyName("token")]
    public string Token { get; set; } = "";

    [JsonPropertyName("user")]
    public UsuarioResponse? User { get; set; }
}