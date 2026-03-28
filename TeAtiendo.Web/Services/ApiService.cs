using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace TeAtiendo.Web.Services
{
    public class ApiService : IApiService
    {
        private readonly HttpClient _http;
        private string _token = string.Empty;

        public ApiService(HttpClient http)
        {
            _http = http;
        }

        public void SetToken(string token)
        {
            _token = token;
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<T?> GetAsync<T>(string url)
        {
            try
            {
                var response = await _http.GetAsync(url);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<T>();
                return default;
            }
            catch
            {
                return default;
            }
        }

        public async Task<T?> PostAsync<T>(string url, object data)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(url, data);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<T>();
                return default;
            }
            catch
            {
                return default;
            }
        }

        public async Task<T?> PutAsync<T>(string url, object data)
        {
            try
            {
                var response = await _http.PutAsJsonAsync(url, data);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<T>();
                return default;
            }
            catch
            {
                return default;
            }
        }

        public async Task<bool> DeleteAsync(string url)
        {
            try
            {
                var response = await _http.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}