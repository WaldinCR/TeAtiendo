using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Reserva;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Enums;
using TeAtiendo.Domain.Interfaces;

namespace TeAtiendo.Application.Services
{
    public sealed class ReservaService : BaseService<Reserva, ReservaDto>, IReservaService
    {
        private readonly IReservaRepository _reservaRepository;

        public ReservaService(TeAtiendo.Persistence.Interface.IUnitOfWork uow, IReservaRepository reservaRepository)
            : base(uow.Reservas, uow)
        {
            _reservaRepository = reservaRepository;
        }

        protected override ReservaDto ToDto(Reserva e) => new()
        {
            Id = e.Id,
            UsuarioId = e.UsuarioId,
            RestauranteId = e.RestauranteId,
            MesaId = e.MesaId,
            DisponibilidadId = e.DisponibilidadId,
            FechaReserva = e.FechaReserva,
            CantidadPersonas = e.CantidadPersonas,
            EstadoReserva = e.EstadoReserva
        };

        protected override void ApplyDto(ReservaDto dto, Reserva e)
        {
            if (dto.UsuarioId == Guid.Empty) throw new ArgumentException("UsuarioId requerido");
            if (dto.RestauranteId == Guid.Empty) throw new ArgumentException("RestauranteId requerido");
            if (dto.MesaId == Guid.Empty) throw new ArgumentException("MesaId requerido");
            if (dto.DisponibilidadId == Guid.Empty) throw new ArgumentException("DisponibilidadId requerido");
            if (dto.CantidadPersonas <= 0) throw new ArgumentException("CantidadPersonas inválida");

            e.UsuarioId = dto.UsuarioId;
            e.RestauranteId = dto.RestauranteId;
            e.MesaId = dto.MesaId;
            e.DisponibilidadId = dto.DisponibilidadId;

            e.FechaReserva = dto.FechaReserva == default ? DateTime.UtcNow : dto.FechaReserva;
            e.CantidadPersonas = dto.CantidadPersonas;
            e.EstadoReserva = dto.EstadoReserva;
        }

        public async Task<IReadOnlyList<ReservaDto>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default)
        {
            var reservas = await _reservaRepository.GetByUsuarioAsync(usuarioId, ct);
            return reservas.Select(ToDto).ToList();
        }

        public async Task<bool> CancelarReservaAsync(Guid reservaId, Guid userId, CancellationToken ct = default)
        {
            var reserva = await Repo.GetByIdAsync(reservaId, ct);
            if (reserva is null) return false;

            await Repo.SoftDeleteAsync(reservaId, userId, ct);
            await Uow.SaveAsync(ct);
            return true;
        }

        public override async Task<ReservaDto> CreateAsync(ReservaDto dto, CancellationToken ct = default)
        {
            dto.EstadoReserva = EstadoReserva.Pendiente;
            return await base.CreateAsync(dto, ct);
        }
    }
}