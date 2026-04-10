using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TeAtiendo.Desktop.Models.Legacy
{
    public class ApiResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("data")]
        public T? Data { get; set; }

        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; } = new();
    }

    public class ApiListResponse<T>
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        [JsonPropertyName("data")]
        public List<T> Data { get; set; } = new();

        [JsonPropertyName("error")]
        public string? Error { get; set; }

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; } = new();
    }
}