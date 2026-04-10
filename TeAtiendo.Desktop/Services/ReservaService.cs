using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.DTOs.Reserva;
using TeAtiendo.Desktop.Models.Legacy;

namespace TeAtiendo.Desktop.Services
{
    public sealed class ReservaService
    {
        private readonly ApiService _api;

        public ReservaService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<ReservaDto>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<List<ReservaDto>>("Reservas");
            return response ?? new List<ReservaDto>();
        }

        public async Task<List<ReservaDto>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            var response = await _api.GetAsync<List<ReservaDto>>($"Reservas/usuario/{usuarioId}");
            return response ?? new List<ReservaDto>();
        }

        public async Task<bool> CancelarAsync(int reservaId, int userId)
        {
            var response = await _api.PostAsync<object, ApiResponse<object>>(
                $"Reservas/{reservaId}/cancelar?userId={userId}",
                new { }
            );

            return response != null;
        }
    }
}