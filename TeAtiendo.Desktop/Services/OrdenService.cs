using TeAtiendo.Desktop.Models;
using TeAtiendo.Desktop.Models.Responses;

namespace TeAtiendo.Desktop.Services
{
    public class OrdenService
    {
        private readonly ApiService _api;

        public OrdenService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<OrdenResponse>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<OrdenResponse>>("Ordenes");
            return response?.Data ?? new List<OrdenResponse>();
        }

        public async Task<List<OrdenResponse>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            var response = await _api.GetAsync<ApiListResponse<OrdenResponse>>($"Ordenes/usuario/{usuarioId}");
            return response?.Data ?? new List<OrdenResponse>();
        }

        public async Task<OrdenResponse?> ObtenerPorIdAsync(int id)
        {
            var response = await _api.GetAsync<ApiResponse<OrdenResponse>>($"Ordenes/{id}");
            return response?.Data;
        }

        public async Task<bool> CambiarEstadoAsync(int ordenId, int nuevoEstado)
        {
            var request = new TeAtiendo.Desktop.Models.Requests.CambiarEstadoOrdenRequest
            {
                NuevoEstado = nuevoEstado
            };

            var response = await _api.PatchAsync<ApiResponse<object>>($"Ordenes/{ordenId}/estado", request);
            return response != null;
        }
    }
}