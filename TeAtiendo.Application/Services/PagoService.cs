using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Pago;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Interfaces;

namespace TeAtiendo.Application.Services
{
    public class PagoService : BaseService<PagoDto, PagoDto, PagoDto>, IPagoService
    {
        private readonly IPagoRepository _pagoRepository;

        public PagoService(IPagoRepository pagoRepository)
        {
            _pagoRepository = pagoRepository;
        }

        public override async Task<IEnumerable<PagoDto>> GetAllAsync()
        {
            var pagos = await _pagoRepository.GetAllAsync();

            return pagos.Select(p => new PagoDto
            {
                Id = p.Id,
                OrdenId = p.OrdenId,
                Monto = p.Monto,
                EstadoPago = p.EstadoPago,
                MetodoPago = p.MetodoPago
            });
        }

        public override async Task<PagoDto?> GetByIdAsync(Guid id)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);

            if (pago == null) return null;

            return new PagoDto
            {
                Id = pago.Id,
                OrdenId = pago.OrdenId,
                Monto = pago.Monto,
                EstadoPago = pago.EstadoPago,
                MetodoPago = pago.MetodoPago
            };
        }

        public override async Task<PagoDto> AddAsync(PagoDto dto)
        {
            var pago = new Pago
            {
                OrdenId = dto.OrdenId,
                Monto = dto.Monto,
                EstadoPago = dto.EstadoPago,
                MetodoPago = dto.MetodoPago
            };

            await _pagoRepository.AddAsync(pago);

            return new PagoDto
            {
                Id = pago.Id,
                OrdenId = pago.OrdenId,
                Monto = pago.Monto,
                EstadoPago = pago.EstadoPago,
                MetodoPago = pago.MetodoPago
            };
        }

        public override async Task UpdateAsync(Guid id, PagoDto dto)
        {
            var pago = await _pagoRepository.GetByIdAsync(id);

            if (pago == null)
                throw new Exception("Pago no encontrado.");

            pago.OrdenId = dto.OrdenId;
            pago.Monto = dto.Monto;
            pago.EstadoPago = dto.EstadoPago;
            pago.MetodoPago = dto.MetodoPago;

            await _pagoRepository.UpdateAsync(pago);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await _pagoRepository.DeleteAsync(id);
        }
    }
}