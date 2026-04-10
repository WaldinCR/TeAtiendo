using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Legacy
{

    public class ApiEnvelope<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("data")]
        public T? Data { get; set; }

        [JsonPropertyName("result")]
        public T? Result { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; } = new();

        [JsonIgnore]
        public T? Value => Data is not null ? Data : Result;

    }
}