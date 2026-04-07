using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class NotificacionService
    {
        private readonly ApiService _api;

        public NotificacionService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Notificacion>> ObtenerPorUsuarioAsync(int usuarioId)
        {
            var response = await _api.GetAsync<ApiListResponse<Notificacion>>($"Notificaciones/usuario/{usuarioId}");
            return response?.Data ?? new List<Notificacion>();
        }

        public async Task<List<Notificacion>> ObtenerNoLeidasAsync(int usuarioId)
        {
            var response = await _api.GetAsync<ApiListResponse<Notificacion>>($"Notificaciones/usuario/{usuarioId}/noleidas");
            return response?.Data ?? new List<Notificacion>();
        }

        public async Task<bool> MarcarLeidaAsync(int notificacionId)
        {
            return await _api.PatchSinBodyAsync($"Notificaciones/{notificacionId}/marcar-leida");
        }

        public async Task<Notificacion?> CrearAsync(Notificacion notificacion)
        {
            var response = await _api.PostAsync<ApiResponse<Notificacion>>("Notificaciones", notificacion);
            return response?.Data;
        }
    }
}