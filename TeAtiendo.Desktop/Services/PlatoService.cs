using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Desktop.Models.Legacy;

namespace TeAtiendo.Desktop.Services
{
    public sealed class PlatoService
    {
        private readonly ApiService _api;

        public PlatoService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<PlatoDto>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<PlatoDto>>("Platos");
            return response?.Data ?? new List<PlatoDto>();
        }

        public async Task<List<PlatoDto>> ObtenerPorMenuAsync(Guid menuId)
        {
            var response = await _api.GetAsync<ApiListResponse<PlatoDto>>($"Platos/menu/{menuId}");
            return response?.Data ?? new List<PlatoDto>();
        }

        public async Task<List<PlatoDto>> ObtenerPorCategoriaAsync(Guid categoriaPlatoId)
        {
            var response = await _api.GetAsync<ApiListResponse<PlatoDto>>($"Platos/categoria/{categoriaPlatoId}");
            return response?.Data ?? new List<PlatoDto>();
        }

        public async Task<PlatoDto?> CrearAsync(PlatoDto dto)
        {
            var response = await _api.PostAsync<PlatoDto, ApiResponse<PlatoDto>>("Platos", dto);
            return response?.Data;
        }

        public async Task<PlatoDto?> ActualizarAsync(PlatoDto dto)
        {
            var response = await _api.PutAsync<PlatoDto, ApiResponse<PlatoDto>>($"Platos/{dto.Id}", dto);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            return await _api.DeleteAsync($"Platos/{id}");
        }
    }
}