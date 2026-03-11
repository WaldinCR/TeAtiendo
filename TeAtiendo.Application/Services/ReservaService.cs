using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Reserva;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Interfaces;

namespace TeAtiendo.Application.Services
{
    public class ReservaService : BaseService<ReservaDto, ReservaDto, ReservaDto>, IReservaService
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservaService(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        public override async Task<IEnumerable<ReservaDto>> GetAllAsync()
        {
            var reservas = await _reservaRepository.GetAllAsync();

            return reservas.Select(r => new ReservaDto
            {
                Id = r.Id,
                UsuarioId = r.UsuarioId,
                RestauranteId = r.RestauranteId,
                MesaId = r.MesaId,
                DisponibilidadId = r.DisponibilidadId,
                FechaReserva = r.FechaReserva,
                CantidadPersonas = r.CantidadPersonas,
                EstadoReserva = r.EstadoReserva
            });
        }

        public override async Task<ReservaDto?> GetByIdAsync(Guid id)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);

            if (reserva == null) return null;

            return new ReservaDto
            {
                Id = reserva.Id,
                UsuarioId = reserva.UsuarioId,
                RestauranteId = reserva.RestauranteId,
                MesaId = reserva.MesaId,
                DisponibilidadId = reserva.DisponibilidadId,
                FechaReserva = reserva.FechaReserva,
                CantidadPersonas = reserva.CantidadPersonas,
                EstadoReserva = reserva.EstadoReserva
            };
        }

        public override async Task<ReservaDto> AddAsync(ReservaDto dto)
        {
            var reserva = new Reserva
            {
                UsuarioId = dto.UsuarioId,
                RestauranteId = dto.RestauranteId,
                MesaId = dto.MesaId,
                DisponibilidadId = dto.DisponibilidadId,
                FechaReserva = dto.FechaReserva,
                CantidadPersonas = dto.CantidadPersonas,
                EstadoReserva = dto.EstadoReserva
            };

            await _reservaRepository.AddAsync(reserva);

            return new ReservaDto
            {
                Id = reserva.Id,
                UsuarioId = reserva.UsuarioId,
                RestauranteId = reserva.RestauranteId,
                MesaId = reserva.MesaId,
                DisponibilidadId = reserva.DisponibilidadId,
                FechaReserva = reserva.FechaReserva,
                CantidadPersonas = reserva.CantidadPersonas,
                EstadoReserva = reserva.EstadoReserva
            };
        }

        public override async Task UpdateAsync(Guid id, ReservaDto dto)
        {
            var reserva = await _reservaRepository.GetByIdAsync(id);

            if (reserva == null)
                throw new Exception("Reserva no encontrada.");

            reserva.UsuarioId = dto.UsuarioId;
            reserva.RestauranteId = dto.RestauranteId;
            reserva.MesaId = dto.MesaId;
            reserva.DisponibilidadId = dto.DisponibilidadId;
            reserva.FechaReserva = dto.FechaReserva;
            reserva.CantidadPersonas = dto.CantidadPersonas;
            reserva.EstadoReserva = dto.EstadoReserva;

            await _reservaRepository.UpdateAsync(reserva);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await _reservaRepository.DeleteAsync(id);
        }
    }
}