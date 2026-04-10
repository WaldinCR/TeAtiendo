using TeAtiendo.Desktop.Models.Legacy;
using TeAtiendo.Desktop.Models.Responses;

namespace TeAtiendo.Desktop.Services
{
    public sealed class RestauranteService
    {
        private readonly ApiService _api;
        public RestauranteService(ApiService api) => _api = api;

        public async Task<IReadOnlyList<RestauranteResponse>> GetAllAsync(CancellationToken ct = default)
        {
            var env = await _api.GetAsync<ApiEnvelope<List<RestauranteResponse>>>("api/restaurantes", ct);
            return env?.Data ?? new List<RestauranteResponse>();
        }

        public async Task<RestauranteResponse?> CreateAsync(RestauranteResponse dto, CancellationToken ct = default)
        {
            var env = await _api.PostAsync<RestauranteResponse, ApiEnvelope<RestauranteResponse>>("api/restaurantes", dto, ct);
            return env?.Data;
        }

        public async Task<RestauranteResponse?> UpdateAsync(Guid id, RestauranteResponse dto, CancellationToken ct = default)
        {
            var env = await _api.PutAsync<RestauranteResponse, ApiEnvelope<RestauranteResponse>>($"api/restaurantes/{id}", dto, ct);
            return env?.Data;
        }

        public Task DeleteAsync(Guid id, Guid userId, CancellationToken ct = default)
            => _api.DeleteAsync($"api/restaurantes/{id}?userId={userId}", ct);
    }
}