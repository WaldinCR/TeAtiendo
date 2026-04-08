using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Requests;

public class CambiarEstadoOrdenRequest
{
    [JsonPropertyName("nuevoEstado")]
    public int NuevoEstado { get; set; }
}