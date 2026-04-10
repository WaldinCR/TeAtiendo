using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.DTOs.Menu;
using TeAtiendo.Desktop.Models.Legacy;

namespace TeAtiendo.Desktop.Services
{
    public sealed class MenuService
    {
        private readonly ApiService _api;

        public MenuService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<MenuDto>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<List<MenuDto>>("Menus");
            return response ?? new List<MenuDto>();
        }

        public async Task<MenuDto?> ObtenerPorIdAsync(Guid id)
        {
            var response = await _api.GetAsync<ApiResponse<MenuDto>>($"Menus/{id}");
            return response?.Data;
        }

        public async Task<MenuDto?> CrearAsync(CreateMenuDto dto)
        {
            var response = await _api.PostAsync<CreateMenuDto, ApiResponse<MenuDto>>("Menus", dto);
            return response?.Data;
        }

        public async Task<MenuDto?> ActualizarAsync(Guid id, UpdateMenuDto dto)
        {
            var response = await _api.PutAsync<UpdateMenuDto, ApiResponse<MenuDto>>($"Menus/{id}", dto);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(Guid id)
        {
            return await _api.DeleteAsync($"Menus/{id}");
        }
    }
}