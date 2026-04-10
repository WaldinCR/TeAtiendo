using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Desktop.Models.Legacy;   

namespace TeAtiendo.Desktop.Services
{
    public sealed class DisponibilidadService
    {
        private readonly ApiService _api;

        public DisponibilidadService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<DisponibilidadDto>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<DisponibilidadDto>>("Disponibilidades");
            return response?.Data ?? new List<DisponibilidadDto>();
        }

        public async Task<List<DisponibilidadDto>> ObtenerPorRestauranteAsync(Guid restauranteId)
        {
            var response = await _api.GetAsync<ApiListResponse<DisponibilidadDto>>(
                $"Disponibilidades/restaurante/{restauranteId}"
            );

            return response?.Data ?? new List<DisponibilidadDto>();
        }

        public async Task<DisponibilidadDto?> CrearAsync(DisponibilidadDto disponibilidad)
        {
            var response = await _api.PostAsync<DisponibilidadDto, ApiResponse<DisponibilidadDto>>(
                "Disponibilidades",
                disponibilidad
            );

            return response?.Data;
        }

        public async Task<DisponibilidadDto?> ActualizarAsync(DisponibilidadDto disponibilidad)
        {
            var response = await _api.PutAsync<DisponibilidadDto, ApiResponse<DisponibilidadDto>>(
                $"Disponibilidades/{disponibilidad.Id}",
                disponibilidad
            );

            return response?.Data;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            return await _api.DeleteAsync($"Disponibilidades/{id}");
        }
    }
}