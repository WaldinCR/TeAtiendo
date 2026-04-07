using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class ReservaService
    {
        private readonly ApiService _api;

        public ReservaService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Reserva>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<Reserva>>("Reservas");
            return response?.Data ?? new List<Reserva>();
        }

        public async Task<List<Reserva>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            var response = await _api.GetAsync<ApiListResponse<Reserva>>($"Reservas/usuario/{usuarioId}");
            return response?.Data ?? new List<Reserva>();
        }

        public async Task<bool> CancelarAsync(int reservaId, int userId)
        {
            var response = await _api.PostAsync<ApiResponse<object>>($"Reservas/{reservaId}/cancelar?userId={userId}", new { });
            return response != null;
        }
    }
}