using TeAtiendo.Desktop.Models;

namespace TeAtiendo.Desktop.Services
{
    public class PagoService
    {
        private readonly ApiService _api;

        public PagoService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<Pago>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<Pago>>("Pagos");
            return response?.Data ?? new List<Pago>();
        }

        public async Task<List<Pago>> ObtenerPorOrdenAsync(int ordenId)
        {
            var response = await _api.GetAsync<ApiListResponse<Pago>>($"Pagos/orden/{ordenId}");
            return response?.Data ?? new List<Pago>();
        }

        public async Task<Pago?> CrearAsync(Pago pago)
        {
            var response = await _api.PostAsync<ApiResponse<Pago>>("Pagos", pago);
            return response?.Data;
        }
    }
}