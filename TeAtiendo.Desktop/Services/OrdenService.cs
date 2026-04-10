using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Desktop.Models.Legacy;
using TeAtiendo.Desktop.Models.Requests;
using TeAtiendo.Desktop.Models.Responses;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Desktop.Services
{
    public sealed class OrdenService
    {
        private readonly ApiService _api;

        public OrdenService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<OrdenResponse>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<List<OrdenResponse>>("Ordenes");
            return response ?? new List<OrdenResponse>();
        }

        public async Task<List<OrdenResponse>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            var response = await _api.GetAsync<List<OrdenResponse>>($"Ordenes/usuario/{usuarioId}");
            return response ?? new List<OrdenResponse>();
        }

        public async Task<OrdenResponse?> ObtenerPorIdAsync(int id)
        {
            var response = await _api.GetAsync<ApiResponse<OrdenResponse>>($"Ordenes/{id}");
            return response?.Data;
        }

        public async Task<bool> CambiarEstadoAsync(int ordenId, EstadoOrden nuevoEstado)
        {
            var request = new CambiarEstadoOrdenRequest
            {
                NuevoEstado = nuevoEstado
            };

            var response = await _api.PatchAsync<CambiarEstadoOrdenRequest, ApiResponse<object>>(
                $"Ordenes/{ordenId}/estado",
                request
            );

            return response != null;
        }
    }
}