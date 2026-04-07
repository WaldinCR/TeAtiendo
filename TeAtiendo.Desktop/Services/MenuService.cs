using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class MenuService
    {
        private readonly ApiService _api;

        public MenuService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Menu>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<Menu>>("Menus");
            return response?.Data ?? new List<Menu>();
        }

        public async Task<Menu?> ObtenerPorIdAsync(int id)
        {
            var response = await _api.GetAsync<ApiResponse<Menu>>($"Menus/{id}");
            return response?.Data;
        }

        public async Task<Menu?> CrearAsync(Menu menu)
        {
            var response = await _api.PostAsync<ApiResponse<Menu>>("Menus", menu);
            return response?.Data;
        }

        public async Task<Menu?> ActualizarAsync(Menu menu)
        {
            var response = await _api.PutAsync<ApiResponse<Menu>>($"Menus/{menu.Id}", menu);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _api.DeleteAsync($"Menus/{id}");
        }
    }
}