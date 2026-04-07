using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class MesaService
    {
        private readonly ApiService _api;

        public MesaService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Mesa>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<Mesa>>("Mesas");
            return response?.Data ?? new List<Mesa>();
        }

        public async Task<List<Mesa>> ObtenerPorRestauranteAsync(int restauranteId)
        {
            var response = await _api.GetAsync<ApiListResponse<Mesa>>($"Mesas/restaurante/{restauranteId}");
            return response?.Data ?? new List<Mesa>();
        }

        public async Task<Mesa?> CrearAsync(Mesa mesa)
        {
            var response = await _api.PostAsync<ApiResponse<Mesa>>("Mesas", mesa);
            return response?.Data;
        }

        public async Task<Mesa?> ActualizarAsync(Mesa mesa)
        {
            var response = await _api.PutAsync<ApiResponse<Mesa>>($"Mesas/{mesa.Id}", mesa);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _api.DeleteAsync($"Mesas/{id}");
        }
    }
}