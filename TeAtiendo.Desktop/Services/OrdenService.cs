using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class OrdenService
    {
        private readonly ApiService _api;

        public OrdenService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Orden>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<Orden>>("Ordenes");
            return response?.Data ?? new List<Orden>();
        }

        public async Task<List<Orden>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            var response = await _api.GetAsync<ApiListResponse<Orden>>($"Ordenes/usuario/{usuarioId}");
            return response?.Data ?? new List<Orden>();
        }

        public async Task<Orden?> ObtenerPorIdAsync(int id)
        {
            var response = await _api.GetAsync<ApiResponse<Orden>>($"Ordenes/{id}");
            return response?.Data;
        }

        public async Task<bool> CambiarEstadoAsync(int ordenId, int nuevoEstado)
        {
            var request = new CambiarEstadoOrdenRequest { NuevoEstado = nuevoEstado };
            var response = await _api.PatchAsync<ApiResponse<object>>($"Ordenes/{ordenId}/estado", request);
            return response != null;
        }
    }
}