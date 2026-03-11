using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeAtiendo.Application.DTOs.Restaurante;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Interfaces;

namespace TeAtiendo.Application.Services
{
    public class RestauranteService : IRestauranteService
    {
        private readonly IRestauranteRepository _restauranteRepository;

        public RestauranteService(IRestauranteRepository restauranteRepository)
        {
            _restauranteRepository = restauranteRepository;
        }

        public async Task<IEnumerable<RestauranteDto>> GetAllAsync()
        {
            var restaurantes = await _restauranteRepository.GetAllAsync();

            return restaurantes.Select(r => new RestauranteDto
            {
                IdRestaurante = r.IdRestaurante,
                Nombre = r.Nombre,
                Direccion = r.Direccion,
                Telefono = r.Telefono,
                Correo = r.Correo,
                Estado = r.Estado,
                HorarioApertura = r.HorarioApertura,
                HorarioCierre = r.HorarioCierre
            });
        }

        public async Task<RestauranteDto?> GetByIdAsync(int id)
        {
            var restaurante = await _restauranteRepository.GetByIdAsync(id);

            if (restaurante == null) return null;

            return new RestauranteDto
            {
                IdRestaurante = restaurante.IdRestaurante,
                Nombre = restaurante.Nombre,
                Direccion = restaurante.Direccion,
                Telefono = restaurante.Telefono,
                Correo = restaurante.Correo,
                Estado = restaurante.Estado,
                HorarioApertura = restaurante.HorarioApertura,
                HorarioCierre = restaurante.HorarioCierre
            };
        }

        public async Task<RestauranteDto> AddAsync(RestauranteDto dto)
        {
            var restaurante = new Restaurante
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Estado = dto.Estado,
                HorarioApertura = dto.HorarioApertura,
                HorarioCierre = dto.HorarioCierre
            };

            await _restauranteRepository.AddAsync(restaurante);

            dto.IdRestaurante = restaurante.IdRestaurante;
            return dto;
        }

        public async Task UpdateAsync(int id, RestauranteDto dto)
        {
            var restaurante = await _restauranteRepository.GetByIdAsync(id);

            if (restaurante == null)
                throw new Exception("Restaurante no encontrado.");

            restaurante.Nombre = dto.Nombre;
            restaurante.Direccion = dto.Direccion;
            restaurante.Telefono = dto.Telefono;
            restaurante.Correo = dto.Correo;
            restaurante.Estado = dto.Estado;
            restaurante.HorarioApertura = dto.HorarioApertura;
            restaurante.HorarioCierre = dto.HorarioCierre;

            await _restauranteRepository.UpdateAsync(restaurante);
        }

        public async Task DeleteAsync(int id)
        {
            await _restauranteRepository.DeleteAsync(id);
        }
    }
}