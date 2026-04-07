using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class ModeracionService
    {
        private readonly ApiService _api;

        public ModeracionService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Moderacion>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<Moderacion>>("Moderacion");
            return response?.Data ?? new List<Moderacion>();
        }

        public async Task<List<Moderacion>> ObtenerPorAdminAsync(int adminId)
        {
            var response = await _api.GetAsync<ApiListResponse<Moderacion>>($"Moderacion/admin/{adminId}");
            return response?.Data ?? new List<Moderacion>();
        }

        public async Task<List<Moderacion>> ObtenerPorTipoAsync(string tipoContenido)
        {
            var response = await _api.GetAsync<ApiListResponse<Moderacion>>($"Moderacion/tipo/{Uri.EscapeDataString(tipoContenido)}");
            return response?.Data ?? new List<Moderacion>();
        }

        public async Task<List<Moderacion>> ObtenerPorEstadoAsync(string estado)
        {
            var response = await _api.GetAsync<ApiListResponse<Moderacion>>($"Moderacion/estado/{Uri.EscapeDataString(estado)}");
            return response?.Data ?? new List<Moderacion>();
        }

        public async Task<bool> CambiarEstadoAsync(int id, string nuevoEstado)
        {
            var request = new CambiarEstadoModeracionRequest { NuevoEstado = nuevoEstado };
            var response = await _api.PatchAsync<ApiResponse<object>>($"Moderacion/{id}/estado", request);
            return response != null;
        }

        public async Task<Moderacion?> CrearAsync(Moderacion moderacion)
        {
            var response = await _api.PostAsync<ApiResponse<Moderacion>>("Moderacion", moderacion);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _api.DeleteAsync($"Moderacion/{id}");
        }
    }
}