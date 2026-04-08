using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using TeAtiendo.Desktop.Helpers;

namespace TeAtiendo.Desktop.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        private const string BASE_URL = "http://localhost:5067/api/";

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public ApiService()
        {
            _http = new HttpClient
            {
                BaseAddress = new Uri(BASE_URL),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        private void SetAuthHeader()
        {
            if (!string.IsNullOrEmpty(SessionManager.Token))
            {
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", SessionManager.Token);
            }
        }
        public async Task<T?> GetAsync<T>(string endpoint)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.GetAsync(endpoint);
                var json = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException($"Error {(int)response.StatusCode}: {json}");
                }

                return JsonSerializer.Deserialize<T>(json, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException(
                    $"No se pudo conectar con el servidor. Base={_http.BaseAddress} Detalle={ex.Message}", ex);
            }
        }

        public async Task<T?> PostAsync<T>(string endpoint, object data)
        {
            try
            {
                SetAuthHeader();
                var json = JsonSerializer.Serialize(data, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _http.PostAsync(endpoint, content);
                var responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException($"Error {(int)response.StatusCode}: {responseJson}");
                }

                return JsonSerializer.Deserialize<T>(responseJson, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                var baseUrl = _http.BaseAddress?.ToString() ?? "(null)";
                var detalle = ex.InnerException?.Message ?? ex.Message;

                throw new ApiException(
                    $"No se pudo conectar con el servidor. Base={baseUrl}. Detalle={detalle}", ex);
            }
        }

        public async Task<T?> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                SetAuthHeader();
                var json = JsonSerializer.Serialize(data, _jsonOptions);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _http.PutAsync(endpoint, content);
                var responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException($"Error {(int)response.StatusCode}: {responseJson}");
                }

                return JsonSerializer.Deserialize<T>(responseJson, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("No se pudo conectar con el servidor.", ex);
            }
        }

        public async Task<T?> PatchAsync<T>(string endpoint, object? data = null)
        {
            try
            {
                SetAuthHeader();
                HttpContent? content = null;
                if (data != null)
                {
                    var json = JsonSerializer.Serialize(data, _jsonOptions);
                    content = new StringContent(json, Encoding.UTF8, "application/json");
                }

                var request = new HttpRequestMessage(HttpMethod.Patch, endpoint) { Content = content };
                var response = await _http.SendAsync(request);
                var responseJson = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    throw new ApiException($"Error {(int)response.StatusCode}: {responseJson}");
                }

                return JsonSerializer.Deserialize<T>(responseJson, _jsonOptions);
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("No se pudo conectar con el servidor.", ex);
            }
        }

        public async Task<bool> DeleteAsync(string endpoint)
        {
            try
            {
                SetAuthHeader();
                var response = await _http.DeleteAsync(endpoint);

                if (!response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    throw new ApiException($"Error {(int)response.StatusCode}: {json}");
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("No se pudo conectar con el servidor.", ex);
            }
        }

        public async Task<bool> PatchSinBodyAsync(string endpoint)
        {
            try
            {
                SetAuthHeader();
                var request = new HttpRequestMessage(HttpMethod.Patch, endpoint);
                var response = await _http.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    throw new ApiException($"Error {(int)response.StatusCode}: {json}");
                }

                return true;
            }
            catch (HttpRequestException ex)
            {
                throw new ApiException("No se pudo conectar con el servidor.", ex);
            }
        }
    }

    public class ApiException : Exception
    {
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception inner) : base(message, inner) { }
    }
}