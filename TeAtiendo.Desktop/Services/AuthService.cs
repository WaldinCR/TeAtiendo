using TeAtiendo.Desktop.Models.Requests;
using TeAtiendo.Desktop.Models.Responses;

namespace TeAtiendo.Desktop.Services
{
    public sealed class AuthService
    {
        private readonly ApiService _api;
        public AuthService(ApiService api) => _api = api;

        public async Task<AuthResponse?> LoginAsync(LoginRequest req, CancellationToken ct = default)
        {
            var result = await _api.PostAsync<LoginRequest, AuthResponse>("api/auth/login", req, ct);

            if (result?.Success == true && !string.IsNullOrWhiteSpace(result.Token))
                Helpers.SessionManager.SetSession(result.Token, result.User);

            return result;
        }
    }
}