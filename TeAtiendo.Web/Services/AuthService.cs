using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using TeAtiendo.Web.Models.Requests;
using TeAtiendo.Web.Models.Reponses;

namespace TeAtiendo.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly IApiService _api;
        private readonly ProtectedSessionStorage _storage;

        public AuthService(IApiService api, ProtectedSessionStorage storage)
        {
            _api = api;
            _storage = storage;
        }

        public bool IsAuthenticated { get; private set; }
        public UserInfo? CurrentUser { get; private set; }
        public string? Token { get; private set; }

        public async Task<AuthResponse?> LoginAsync(LoginRequest request)
        {
            var response = await _api.PostAsync<AuthResponse>("api/Auth/login", request);

            if (response is not null && response.Success)
            {
                Token = response.Token;
                CurrentUser = response.User;
                IsAuthenticated = true;

                _api.SetToken(response.Token!);

                await _storage.SetAsync("token", response.Token!);
                await _storage.SetAsync("userId", response.User!.Id.ToString());
                await _storage.SetAsync("userName", response.User.Nombre);
                await _storage.SetAsync("userEmail", response.User.Correo);
                await _storage.SetAsync("userRol", response.User.Rol.ToString());

                // Si es rol Restaurante, buscar su restauranteId
                if (response.User.Rol == 2)
                {
                    try
                    {
                        var restaurantes = await _api.GetAsync<RestauranteApiResponse>("api/Restaurantes");
                        if (restaurantes?.Data != null && restaurantes.Data.Any())
                        {
                            var miRest = restaurantes.Data.FirstOrDefault();
                            if (miRest != null)
                            {
                                await _storage.SetAsync("restauranteId", miRest.Id.ToString());
                            }
                        }
                    }
                    catch { }
                }
            }

            return response;
        }

        public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
        {
            var response = await _api.PostAsync<AuthResponse>("api/Auth/register", request);
            return response;
        }

        public async Task LoadSession()
        {
            try
            {
                var tokenResult = await _storage.GetAsync<string>("token");
                if (tokenResult.Success && !string.IsNullOrEmpty(tokenResult.Value))
                {
                    Token = tokenResult.Value;
                    _api.SetToken(Token);
                    IsAuthenticated = true;

                    var idResult = await _storage.GetAsync<string>("userId");
                    var nameResult = await _storage.GetAsync<string>("userName");
                    var emailResult = await _storage.GetAsync<string>("userEmail");
                    var rolResult = await _storage.GetAsync<string>("userRol");

                    var userId = Guid.Empty;
                    try { userId = Guid.Parse(idResult.Value ?? ""); } catch { }

                    var rol = 1;
                    try { rol = int.Parse(rolResult.Value ?? "1"); } catch { }

                    CurrentUser = new UserInfo
                    {
                        Id = userId,
                        Nombre = nameResult.Value ?? "Usuario",
                        Correo = emailResult.Value ?? "",
                        Rol = rol
                    };
                }
            }
            catch
            {
                IsAuthenticated = false;
            }
        }

        public async Task Logout()
        {
            Token = null;
            CurrentUser = null;
            IsAuthenticated = false;

            await _storage.DeleteAsync("token");
            await _storage.DeleteAsync("userId");
            await _storage.DeleteAsync("userName");
            await _storage.DeleteAsync("userEmail");
            await _storage.DeleteAsync("userRol");
            await _storage.DeleteAsync("restauranteId");
        }
    }
}