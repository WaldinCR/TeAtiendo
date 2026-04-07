using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class AuthService
    {
        private readonly ApiService _api;

        public AuthService(ApiService api)
        {
            _api = api;
        }

        public async Task<LoginResponse?> LoginAsync(string correo, string password)
        {
            var request = new LoginRequest
            {
                Correo = correo,
                Password = password
            };

            return await _api.PostAsync<LoginResponse>("Auth/login", request);
        }

        public async Task<ApiResponse<object>?> RegisterAsync(RegisterRequest request)
        {
            return await _api.PostAsync<ApiResponse<object>>("Auth/register", request);
        }
    }
}