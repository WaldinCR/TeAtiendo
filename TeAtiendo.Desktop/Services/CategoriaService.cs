using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class CategoriaService
    {
        private readonly ApiService _api;

        public CategoriaService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<CategoriaPlato>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<CategoriaPlato>>("Categoriasplato");
            return response?.Data ?? new List<CategoriaPlato>();
        }

        public async Task<CategoriaPlato?> CrearAsync(CategoriaPlato categoria)
        {
            var response = await _api.PostAsync<ApiResponse<CategoriaPlato>>("Categoriasplato", categoria);
            return response?.Data;
        }

        public async Task<CategoriaPlato?> ActualizarAsync(CategoriaPlato categoria)
        {
            var response = await _api.PutAsync<ApiResponse<CategoriaPlato>>($"Categoriasplato/{categoria.Id}", categoria);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _api.DeleteAsync($"Categoriasplato/{id}");
        }
    }
}