using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class UsuarioService
    {
        private readonly ApiService _api;

        public UsuarioService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Usuario>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<Usuario>>("Usuarios");
            return response?.Data ?? new List<Usuario>();
        }

        public async Task<Usuario?> ObtenerPorIdAsync(int id)
        {
            var response = await _api.GetAsync<ApiResponse<Usuario>>($"Usuarios/{id}");
            return response?.Data;
        }

        public async Task<Usuario?> ObtenerPorEmailAsync(string correo)
        {
            var response = await _api.GetAsync<ApiResponse<Usuario>>($"Usuarios/email/{Uri.EscapeDataString(correo)}");
            return response?.Data;
        }

        public async Task<Usuario?> ActualizarAsync(Usuario usuario)
        {
            var response = await _api.PutAsync<ApiResponse<Usuario>>($"Usuarios/{usuario.Id}", usuario);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _api.DeleteAsync($"Usuarios/{id}");
        }
    }
}