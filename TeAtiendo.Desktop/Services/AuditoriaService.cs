using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class AuditoriaService
    {
        private readonly ApiService _api;

        public AuditoriaService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Auditoria>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<Auditoria>>("Auditorias");
            return response?.Data ?? new List<Auditoria>();
        }

        public async Task<List<Auditoria>> ObtenerPorAdminAsync(int adminId)
        {
            var response = await _api.GetAsync<ApiListResponse<Auditoria>>($"Auditorias/admin/{adminId}");
            return response?.Data ?? new List<Auditoria>();
        }

        public async Task<List<Auditoria>> ObtenerPorModuloAsync(string modulo)
        {
            var response = await _api.GetAsync<ApiListResponse<Auditoria>>($"Auditorias/modulo/{Uri.EscapeDataString(modulo)}");
            return response?.Data ?? new List<Auditoria>();
        }

        public async Task<List<Auditoria>> ObtenerPorRangoAsync(DateTime desde, DateTime hasta)
        {
            var desdeStr = desde.ToString("yyyy-MM-dd");
            var hastaStr = hasta.ToString("yyyy-MM-dd");
            var response = await _api.GetAsync<ApiListResponse<Auditoria>>($"Auditorias/rango?desde={desdeStr}&hasta={hastaStr}");
            return response?.Data ?? new List<Auditoria>();
        }

        public async Task<Auditoria?> CrearAsync(Auditoria auditoria)
        {
            var response = await _api.PostAsync<ApiResponse<Auditoria>>("Auditorias", auditoria);
            return response?.Data;
        }
    }
}