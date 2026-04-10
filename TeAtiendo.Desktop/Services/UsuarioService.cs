using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.DTOs.Usuario;
using TeAtiendo.Desktop.Models.Legacy;

namespace TeAtiendo.Desktop.Services
{
    public sealed class UsuarioService
    {
        private readonly ApiService _api;

        public UsuarioService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<UsuarioDto>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<List<UsuarioDto>>("Usuarios");
            return response ?? new List<UsuarioDto>();
        }

        public async Task<UsuarioDto?> ObtenerPorIdAsync(Guid id)
        {
            var response = await _api.GetAsync<ApiResponse<UsuarioDto>>($"Usuarios/{id}");
            return response?.Data;
        }

        public async Task<UsuarioDto?> ObtenerPorEmailAsync(string correo)
        {
            var response = await _api.GetAsync<ApiResponse<UsuarioDto>>(
                $"Usuarios/email/{Uri.EscapeDataString(correo)}"
            );
            return response?.Data;
        }

        public async Task<UsuarioDto?> ActualizarAsync(Guid id, UpdateUsuarioDto dto)
        {
            var response = await _api.PutAsync<UpdateUsuarioDto, ApiResponse<UsuarioDto>>($"Usuarios/{id}", dto);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            return await _api.DeleteAsync($"Usuarios/{id}");
        }
    }
}