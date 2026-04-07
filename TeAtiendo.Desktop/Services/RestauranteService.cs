using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class RestauranteService
    {
        private readonly ApiService _api;

        public RestauranteService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Restaurante>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<Restaurante>>("Restaurantes");
            return response?.Data ?? new List<Restaurante>();
        }

        public async Task<Restaurante?> ObtenerPorIdAsync(int id)
        {
            var response = await _api.GetAsync<ApiResponse<Restaurante>>($"Restaurantes/{id}");
            return response?.Data;
        }

        public async Task<List<Restaurante>> BuscarAsync(string nombre)
        {
            var response = await _api.GetAsync<ApiListResponse<Restaurante>>($"Restaurantes/buscar?nombre={Uri.EscapeDataString(nombre)}");
            return response?.Data ?? new List<Restaurante>();
        }

        public async Task<Restaurante?> CrearAsync(Restaurante restaurante)
        {
            var response = await _api.PostAsync<ApiResponse<Restaurante>>("Restaurantes", restaurante);
            return response?.Data;
        }

        public async Task<Restaurante?> ActualizarAsync(Restaurante restaurante)
        {
            var response = await _api.PutAsync<ApiResponse<Restaurante>>($"Restaurantes/{restaurante.Id}", restaurante);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _api.DeleteAsync($"Restaurantes/{id}");
        }
    }
}