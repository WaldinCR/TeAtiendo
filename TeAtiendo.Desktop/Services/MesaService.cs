using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.DTOs; 
using TeAtiendo.Desktop.Models.Legacy; 

namespace TeAtiendo.Desktop.Services
{
    public sealed class MesaService
    {
        private readonly ApiService _api;

        public MesaService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<MesaDto>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<List<MesaDto>>("Mesas");
            return response ?? new List<MesaDto>();
        }

        public async Task<List<MesaDto>> ObtenerPorRestauranteAsync(int restauranteId)
        {
            var response = await _api.GetAsync<List<MesaDto>>($"Mesas/restaurante/{restauranteId}");
            return response ?? new List<MesaDto>();
        }

        public async Task<MesaDto?> CrearAsync(MesaDto mesa)
        {
            var response = await _api.PostAsync<MesaDto, ApiResponse<MesaDto>>("Mesas", mesa);
            return response?.Data;
        }

        public async Task<MesaDto?> ActualizarAsync(MesaDto mesa)
        {
            var response = await _api.PutAsync<MesaDto, ApiResponse<MesaDto>>($"Mesas/{mesa.Id}", mesa);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _api.DeleteAsync($"Mesas/{id}");
        }
    }
}