using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class DisponibilidadService
    {
        private readonly ApiService _api;

        public DisponibilidadService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Disponibilidad>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<Disponibilidad>>("Disponibilidades");
            return response?.Data ?? new List<Disponibilidad>();
        }

        public async Task<List<Disponibilidad>> ObtenerPorRestauranteAsync(int restauranteId)
        {
            var response = await _api.GetAsync<ApiListResponse<Disponibilidad>>($"Disponibilidades/restaurante/{restauranteId}");
            return response?.Data ?? new List<Disponibilidad>();
        }

        public async Task<Disponibilidad?> CrearAsync(Disponibilidad disponibilidad)
        {
            var response = await _api.PostAsync<ApiResponse<Disponibilidad>>("Disponibilidades", disponibilidad);
            return response?.Data;
        }

        public async Task<Disponibilidad?> ActualizarAsync(Disponibilidad disponibilidad)
        {
            var response = await _api.PutAsync<ApiResponse<Disponibilidad>>($"Disponibilidades/{disponibilidad.Id}", disponibilidad);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _api.DeleteAsync($"Disponibilidades/{id}");
        }
    }
}