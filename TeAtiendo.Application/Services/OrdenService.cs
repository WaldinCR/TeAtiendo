using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Orden;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Interfaces;

namespace TeAtiendo.Application.Services
{
    public class OrdenService : BaseService<OrdenDto, SaveOrdenDto, UpdateOrdenDto>, IOrdenService
    {
        private readonly IOrdenRepository _ordenRepository;

        public OrdenService(IOrdenRepository ordenRepository)
        {
            _ordenRepository = ordenRepository;
        }

        public override async Task<IEnumerable<OrdenDto>> GetAllAsync()
        {
            var ordenes = await _ordenRepository.GetAllAsync();

            return ordenes.Select(o => new OrdenDto
            {
                Id = o.Id,
                UsuarioId = o.UsuarioId,
                RestauranteId = o.RestauranteId,
                EstadoOrden = o.EstadoOrden,
                Total = o.Total
            });
        }

        public override async Task<OrdenDto?> GetByIdAsync(Guid id)
        {
            var orden = await _ordenRepository.GetByIdAsync(id);

            if (orden == null) return null;

            return new OrdenDto
            {
                Id = orden.Id,
                UsuarioId = orden.UsuarioId,
                RestauranteId = orden.RestauranteId,
                EstadoOrden = orden.EstadoOrden,
                Total = orden.Total
            };
        }

        public override async Task<OrdenDto> AddAsync(SaveOrdenDto dto)
        {
            var orden = new Orden
            {
                UsuarioId = dto.UsuarioId,
                RestauranteId = dto.RestauranteId,
                EstadoOrden = dto.EstadoOrden,
                Total = dto.Total
            };

            await _ordenRepository.AddAsync(orden);

            return new OrdenDto
            {
                Id = orden.Id,
                UsuarioId = orden.UsuarioId,
                RestauranteId = orden.RestauranteId,
                EstadoOrden = orden.EstadoOrden,
                Total = orden.Total
            };
        }

        public override async Task UpdateAsync(Guid id, UpdateOrdenDto dto)
        {
            var orden = await _ordenRepository.GetByIdAsync(id);

            if (orden == null)
                throw new Exception("Orden no encontrada.");

            orden.UsuarioId = dto.UsuarioId;
            orden.RestauranteId = dto.RestauranteId;
            orden.EstadoOrden = dto.EstadoOrden;
            orden.Total = dto.Total;

            await _ordenRepository.UpdateAsync(orden);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await _ordenRepository.DeleteAsync(id);
        }
    }
}