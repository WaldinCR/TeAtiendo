using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using TeAtiendo.Desktop.Helpers;
using TeAtiendo.Desktop.Models.Legacy;
using TeAtiendo.Desktop.Services.Json;

namespace TeAtiendo.Desktop.Services
{
    public sealed class ApiService
    {
        private static readonly Lazy<HttpClient> _lazy = new(() =>
        {
            var c = new HttpClient { Timeout = TimeSpan.FromSeconds(60) };
            return c;
        });

        private readonly string _baseUrl;
        private readonly JsonSerializerOptions _json;

        public ApiService(string baseUrl)
        {
            _baseUrl = baseUrl.TrimEnd('/');

            _json = new JsonSerializerOptions(JsonSerializerDefaults.Web)
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };
            _json.Converters.Add(new TimeOnlyJsonConverter());
        }

        private HttpClient Client => _lazy.Value;

        private HttpRequestMessage Create(HttpMethod method, string route)
        {
            var req = new HttpRequestMessage(method, $"{_baseUrl}/{route.TrimStart('/')}");

            if (!string.IsNullOrWhiteSpace(SessionManager.JwtToken))
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", SessionManager.JwtToken);

            req.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return req;
        }

        public Task<T?> GetAsync<T>(string route, CancellationToken ct = default)
            => SendAsync<T>(HttpMethod.Get, route, null, ct);

        public Task<TResponse?> PostAsync<TRequest, TResponse>(string route, TRequest body, CancellationToken ct = default)
            => SendAsync<TResponse>(HttpMethod.Post, route, body!, ct);

        public Task<TResponse?> PutAsync<TRequest, TResponse>(string route, TRequest body, CancellationToken ct = default)
            => SendAsync<TResponse>(HttpMethod.Put, route, body!, ct);

        public Task<TResponse?> PatchAsync<TRequest, TResponse>(string route, TRequest body, CancellationToken ct = default)
            => SendAsync<TResponse>(HttpMethod.Patch, route, body!, ct);

        public Task<TResponse?> PatchSinBodyAsync<TResponse>(string route, CancellationToken ct = default)
            => SendAsync<TResponse>(HttpMethod.Patch, route, null, ct);

        public async Task<bool> DeleteAsync(string route, CancellationToken ct = default)
        {
            using var req = Create(HttpMethod.Delete, route);
            using var res = await Client.SendAsync(req, ct);
            var text = res.Content is null ? null : await res.Content.ReadAsStringAsync(ct);

            if (!res.IsSuccessStatusCode)
            {
                var msg = ExtractMessage(text) ?? $"HTTP {(int)res.StatusCode} ({res.StatusCode})";
                throw new ApiServiceException(res.StatusCode, msg, text);
            }

            return true;
        }

        private async Task<T?> SendAsync<T>(HttpMethod method, string route, object? body, CancellationToken ct)
        {
            using var req = Create(method, route);

            if (body is not null)
            {
                var payload = JsonSerializer.Serialize(body, _json);
                req.Content = new StringContent(payload, Encoding.UTF8, "application/json");
            }

            using var res = await Client.SendAsync(req, ct);
            var text = res.Content is null ? null : await res.Content.ReadAsStringAsync(ct);

            if (!res.IsSuccessStatusCode)
            {
                var msg = ExtractMessage(text) ?? $"HTTP {(int)res.StatusCode} ({res.StatusCode})";
                throw new ApiServiceException(res.StatusCode, msg, text);
            }

            if (typeof(T) == typeof(object) || string.IsNullOrWhiteSpace(text))
                return default;

            return JsonSerializer.Deserialize<T>(text, _json);
        }

        private string? ExtractMessage(string? body)
        {
            if (string.IsNullOrWhiteSpace(body)) return null;
    
            try
            {
                var env = JsonSerializer.Deserialize<ApiEnvelope<object>>(body, _json);
                if (env != null)
                {
                    if (env.Errors is { Count: > 0 })
                        return (env.Message ?? "Errores") + "\n- " + string.Join("\n- ", env.Errors);

                    if (!string.IsNullOrWhiteSpace(env.Error))
                        return (env.Message ?? "Error") + $"\nDetalle: {env.Error}";

                    if (!string.IsNullOrWhiteSpace(env.Message))
                        return env.Message;
                }
            }
            catch { }

            try
            {
                using var doc = JsonDocument.Parse(body);
                if (doc.RootElement.TryGetProperty("message", out var m))
                    return m.GetString();
            }
            catch { }

            return null;
        }
    }
}