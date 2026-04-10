using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Desktop.Models.Legacy;

namespace TeAtiendo.Desktop.Services
{
    public sealed class NotificacionService
    {
        private readonly ApiService _api;

        public NotificacionService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<NotificacionDto>> ObtenerPorUsuarioAsync(Guid usuarioId)
        {
            var response =
                await _api.GetAsync<List<NotificacionDto>>($"Notificaciones/usuario/{usuarioId}");

            return response ?? new List<NotificacionDto>();
        }

        public async Task<List<NotificacionDto>> ObtenerNoLeidasAsync(Guid usuarioId)
        {
            var response =
                await _api.GetAsync<List<NotificacionDto>>($"Notificaciones/usuario/{usuarioId}/noleidas");

            return response ?? new List<NotificacionDto>();
        }

        public async Task<bool> MarcarLeidaAsync(Guid notificacionId)
        {
            var response = await _api.PatchSinBodyAsync<ApiResponse<object>>(
                $"Notificaciones/{notificacionId}/marcar-leida"
            );

            return response != null;
        }

        public async Task<NotificacionDto?> CrearAsync(NotificacionDto dto)
        {
            var response = await _api.PostAsync<NotificacionDto, ApiResponse<NotificacionDto>>(
                "Notificaciones",
                dto
            );

            return response?.Data;
        }
    }
}