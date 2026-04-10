using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.DTOs.Pago;
using TeAtiendo.Desktop.Models.Legacy;

namespace TeAtiendo.Desktop.Services
{
    public sealed class PagoService
    {
        private readonly ApiService _api;

        public PagoService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<PagoDto>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<PagoDto>>("Pagos");
            return response?.Data ?? new List<PagoDto>();
        }

        public async Task<List<PagoDto>> ObtenerPorOrdenAsync(Guid ordenId)
        {
            var response = await _api.GetAsync<ApiListResponse<PagoDto>>($"Pagos/orden/{ordenId}");
            return response?.Data ?? new List<PagoDto>();
        }

        public async Task<PagoDto?> CrearAsync(PagoDto dto)
        {
            var response = await _api.PostAsync<PagoDto, ApiResponse<PagoDto>>("Pagos", dto);
            return response?.Data;
        }
    }
}