using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Interface;

namespace TeAtiendo.Application.Services
{
    public sealed class DisponibilidadService : BaseService<Disponibilidad, DisponibilidadDto>, IDisponibilidadService
    {
        private readonly IDisponibilidadRepository _disponibilidadRepository;

        public DisponibilidadService(IUnitOfWork uow, IDisponibilidadRepository disponibilidadRepository)
            : base(disponibilidadRepository, uow)
        {
            _disponibilidadRepository = disponibilidadRepository;
        }

        protected override DisponibilidadDto ToDto(Disponibilidad e) => new()
        {
            Id = e.Id,
            RestauranteId = e.RestauranteId,
            Fecha = e.Fecha,
            HoraInicio = e.HoraInicio,
            HoraFin = e.HoraFin,
            Estado = e.Estado,
            FechaCreacion = e.CreationDate
        };

        protected override void ApplyDto(DisponibilidadDto dto, Disponibilidad e)
        {
            if (dto.RestauranteId == Guid.Empty) throw new ArgumentException("RestauranteId requerido");
            if (dto.Fecha == default) throw new ArgumentException("Fecha requerida");
            if (dto.HoraInicio >= dto.HoraFin) throw new ArgumentException("HoraInicio debe ser menor a HoraFin");

            e.RestauranteId = dto.RestauranteId;
            e.Fecha = dto.Fecha;
            e.HoraInicio = dto.HoraInicio;
            e.HoraFin = dto.HoraFin;
            e.Estado = dto.Estado;
        }

        public async Task<IReadOnlyList<DisponibilidadDto>> GetByRestauranteAsync(Guid restauranteId, CancellationToken ct = default)
        {
            var disponibilidades = await _disponibilidadRepository.GetByRestauranteAsync(restauranteId, ct);
            return disponibilidades.Select(ToDto).ToList();
        }

        public async Task<IReadOnlyList<DisponibilidadDto>> GetByFechaAsync(DateTime fecha, CancellationToken ct = default)
        {
            var disponibilidades = await _disponibilidadRepository.GetByFechaAsync(fecha, ct);
            return disponibilidades.Select(ToDto).ToList();
        }

        public async Task<IReadOnlyList<DisponibilidadDto>> GetByRestauranteAndFechaAsync(Guid restauranteId, DateTime fecha, CancellationToken ct = default)
        {
            var disponibilidades = await _disponibilidadRepository.GetByRestauranteAndFechaAsync(restauranteId, fecha, ct);
            return disponibilidades.Select(ToDto).ToList();
        }
    }
}