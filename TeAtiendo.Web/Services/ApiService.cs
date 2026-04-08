using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TeAtiendo.Web.Models;

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

        // Asegurar que el token está seteado antes de cada llamada
        private void EnsureToken()
        {
            if (!string.IsNullOrEmpty(_token) && _http.DefaultRequestHeaders.Authorization == null)
            {
                _http.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", _token);
            }
        }

        // metodos 

        public async Task<T?> GetAsync<T>(string url)
        {
            try
            {
                EnsureToken();
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
                EnsureToken();
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
                EnsureToken();
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
                EnsureToken();
                var response = await _http.DeleteAsync(url);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        public async Task<T?> PatchAsync<T>(string url, object data)
        {
            try
            {
                EnsureToken();
                var request = new HttpRequestMessage(HttpMethod.Patch, url)
                {
                    Content = JsonContent.Create(data)
                };
                var response = await _http.SendAsync(request);
                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadFromJsonAsync<T>();
                return default;
            }
            catch
            {
                return default;
            }
        }

        // Métodos CON manejo de errores
       

        public async Task<ApiResult<T>> GetWithResultAsync<T>(string url)
        {
            try
            {
                EnsureToken();
                var response = await _http.GetAsync(url);
                return await HandleResponse<T>(response);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Failure("Error de conexion: " + ex.Message, 0);
            }
            catch (TaskCanceledException)
            {
                return ApiResult<T>.Failure("La solicitud tomo demasiado tiempo (timeout)", 408);
            }
            catch (Exception ex)
            {
                return ApiResult<T>.Failure("Error inesperado: " + ex.Message, 0);
            }
        }

        public async Task<ApiResult<T>> PostWithResultAsync<T>(string url, object data)
        {
            try
            {
                EnsureToken();
                var response = await _http.PostAsJsonAsync(url, data);
                return await HandleResponse<T>(response);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Failure("Error de conexion: " + ex.Message, 0);
            }
            catch (TaskCanceledException)
            {
                return ApiResult<T>.Failure("La solicitud tomo demasiado tiempo (timeout)", 408);
            }
            catch (Exception ex)
            {
                return ApiResult<T>.Failure("Error inesperado: " + ex.Message, 0);
            }
        }

        public async Task<ApiResult<T>> PutWithResultAsync<T>(string url, object data)
        {
            try
            {
                EnsureToken();
                var response = await _http.PutAsJsonAsync(url, data);
                return await HandleResponse<T>(response);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Failure("Error de conexion: " + ex.Message, 0);
            }
            catch (TaskCanceledException)
            {
                return ApiResult<T>.Failure("La solicitud tomo demasiado tiempo (timeout)", 408);
            }
            catch (Exception ex)
            {
                return ApiResult<T>.Failure("Error inesperado: " + ex.Message, 0);
            }
        }

        public async Task<ApiResult<T>> PatchWithResultAsync<T>(string url, object data)
        {
            try
            {
                EnsureToken();
                var request = new HttpRequestMessage(HttpMethod.Patch, url)
                {
                    Content = JsonContent.Create(data)
                };
                var response = await _http.SendAsync(request);
                return await HandleResponse<T>(response);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<T>.Failure("Error de conexion: " + ex.Message, 0);
            }
            catch (TaskCanceledException)
            {
                return ApiResult<T>.Failure("La solicitud tomo demasiado tiempo (timeout)", 408);
            }
            catch (Exception ex)
            {
                return ApiResult<T>.Failure("Error inesperado: " + ex.Message, 0);
            }
        }

        public async Task<ApiResult<bool>> DeleteWithResultAsync(string url)
        {
            try
            {
                EnsureToken();
                var response = await _http.DeleteAsync(url);
                if (response.IsSuccessStatusCode)
                    return ApiResult<bool>.Success(true, (int)response.StatusCode);

                var errorMsg = await GetErrorMessage(response);
                return ApiResult<bool>.Failure(errorMsg, (int)response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                return ApiResult<bool>.Failure("Error de conexion: " + ex.Message, 0);
            }
            catch (TaskCanceledException)
            {
                return ApiResult<bool>.Failure("La solicitud tomo demasiado tiempo (timeout)", 408);
            }
            catch (Exception ex)
            {
                return ApiResult<bool>.Failure("Error inesperado: " + ex.Message, 0);
            }
        }

 
        // Manejo centralizado de respuestas HTTP
       

        private async Task<ApiResult<T>> HandleResponse<T>(HttpResponseMessage response)
        {
            var statusCode = (int)response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var data = await response.Content.ReadFromJsonAsync<T>();
                    return ApiResult<T>.Success(data!, statusCode);
                }
                catch
                {
                    return ApiResult<T>.Success(default!, statusCode);
                }
            }

            var errorMsg = await GetErrorMessage(response);
            return ApiResult<T>.Failure(errorMsg, statusCode);
        }

        private async Task<string> GetErrorMessage(HttpResponseMessage response)
        {
            var statusCode = (int)response.StatusCode;

            try
            {
                var body = await response.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(body) && body.Length < 500)
                    return body;
            }
            catch { }

            return statusCode switch
            {
                400 => "Datos invalidos. Verifica la informacion enviada.",
                401 => "No autorizado. Inicia sesion nuevamente.",
                403 => "No tienes permisos para realizar esta accion.",
                404 => "El recurso solicitado no fue encontrado.",
                409 => "Conflicto: el recurso ya existe o esta en uso.",
                500 => "Error interno del servidor. Intenta mas tarde.",
                502 => "El servidor no esta disponible temporalmente.",
                503 => "Servicio no disponible. Intenta mas tarde.",
                _ => "Error HTTP " + statusCode
            };
        }
    }
}