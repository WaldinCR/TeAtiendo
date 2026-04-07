using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class PlatoService
    {
        private readonly ApiService _api;

        public PlatoService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Plato>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<Plato>>("Platos");
            return response?.Data ?? new List<Plato>();
        }

        public async Task<List<Plato>> ObtenerPorMenuAsync(int menuId)
        {
            var response = await _api.GetAsync<ApiListResponse<Plato>>($"Platos/menu/{menuId}");
            return response?.Data ?? new List<Plato>();
        }

        public async Task<List<Plato>> ObtenerPorCategoriaAsync(int categoriaId)
        {
            var response = await _api.GetAsync<ApiListResponse<Plato>>($"Platos/categoria/{categoriaId}");
            return response?.Data ?? new List<Plato>();
        }

        public async Task<Plato?> CrearAsync(Plato plato)
        {
            var response = await _api.PostAsync<ApiResponse<Plato>>("Platos", plato);
            return response?.Data;
        }

        public async Task<Plato?> ActualizarAsync(Plato plato)
        {
            var response = await _api.PutAsync<ApiResponse<Plato>>($"Platos/{plato.Id}", plato);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _api.DeleteAsync($"Platos/{id}");
        }
    }
}