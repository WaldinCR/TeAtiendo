using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.DTOs.Moderacion;
using TeAtiendo.Desktop.Models.Legacy;

namespace TeAtiendo.Desktop.Services
{
    public sealed class ModeracionService
    {
        private readonly ApiService _api;

        public ModeracionService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<ModeracionContenidoDto>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<ModeracionContenidoDto>>("Moderacion");
            return response?.Data ?? new List<ModeracionContenidoDto>();
        }

        public async Task<List<ModeracionContenidoDto>> ObtenerPorAdminAsync(Guid adminId)
        {
            var response = await _api.GetAsync<ApiListResponse<ModeracionContenidoDto>>($"Moderacion/admin/{adminId}");
            return response?.Data ?? new List<ModeracionContenidoDto>();
        }

        public async Task<List<ModeracionContenidoDto>> ObtenerPorTipoAsync(string tipoContenido)
        {
            var response = await _api.GetAsync<ApiListResponse<ModeracionContenidoDto>>(
                $"Moderacion/tipo/{Uri.EscapeDataString(tipoContenido)}"
            );

            return response?.Data ?? new List<ModeracionContenidoDto>();
        }

        public async Task<List<ModeracionContenidoDto>> ObtenerPorEstadoAsync(string estado)
        {
            var response = await _api.GetAsync<ApiListResponse<ModeracionContenidoDto>>(
                $"Moderacion/estado/{Uri.EscapeDataString(estado)}"
            );

            return response?.Data ?? new List<ModeracionContenidoDto>();
        }

        // Si tu API espera { nuevoEstado: "..." }, usamos este DTO interno.
        private sealed class CambiarEstadoModeracionDto
        {
            public string NuevoEstado { get; set; } = string.Empty;
        }

        public async Task<bool> CambiarEstadoAsync(Guid id, string nuevoEstado)
        {
            var request = new CambiarEstadoModeracionDto { NuevoEstado = nuevoEstado };

            var response = await _api.PatchAsync<CambiarEstadoModeracionDto, ApiResponse<object>>(
                $"Moderacion/{id}/estado",
                request
            );

            return response != null;
        }

        public async Task<ModeracionContenidoDto?> CrearAsync(ModeracionContenidoDto dto)
        {
            var response = await _api.PostAsync<ModeracionContenidoDto, ApiResponse<ModeracionContenidoDto>>(
                "Moderacion",
                dto
            );

            return response?.Data;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            return await _api.DeleteAsync($"Moderacion/{id}");
        }
    }
}