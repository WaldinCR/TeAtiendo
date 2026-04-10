using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Desktop.Models.Legacy;

namespace TeAtiendo.Desktop.Services
{
    public sealed class CategoriaService
    {
        private readonly ApiService _api;

        public CategoriaService(ApiService api)
        {
            _api = api;
        }

        public async Task<List<CategoriaplatoDto>> ObtenerTodosAsync()
        {
            var response = await _api.GetAsync<ApiListResponse<CategoriaplatoDto>>("Categoriasplato");
            return response?.Data ?? new List<CategoriaplatoDto>();
        }

        public async Task<CategoriaplatoDto?> CrearAsync(CategoriaplatoDto categoria)
        {
            var response = await _api.PostAsync<CategoriaplatoDto, ApiResponse<CategoriaplatoDto>>("Categoriasplato", categoria);
            return response?.Data;
        }

        public async Task<CategoriaplatoDto?> ActualizarAsync(CategoriaplatoDto categoria)
        {
            var response = await _api.PutAsync<CategoriaplatoDto, ApiResponse<CategoriaplatoDto>>($"Categoriasplato/{categoria.Id}", categoria);
            return response?.Data;
        }

        public async Task<bool> EliminarAsync(int id)
        {
            return await _api.DeleteAsync($"Categoriasplato/{id}");
        }
    }
}