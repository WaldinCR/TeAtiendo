using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Interface;

namespace TeAtiendo.Application.Services
{
    public sealed class MesaService : BaseService<Mesa, MesaDto>, IMesaService
    {
        private readonly IMesaRepository _mesaRepository;

        public MesaService(IUnitOfWork uow, IMesaRepository mesaRepository)
            : base(mesaRepository, uow)
        {
            _mesaRepository = mesaRepository;
        }

        protected override MesaDto ToDto(Mesa e) => new()
        {
            Id = e.Id,
            RestauranteId = e.RestauranteId,
            NumeroMesa = e.Numero,
            Capacidad = e.Capacidad,
            Disponible = true,
            FechaCreacion = e.CreationDate
        };

        protected override void ApplyDto(MesaDto dto, Mesa e)
        {
            if (dto.RestauranteId == Guid.Empty) throw new ArgumentException("RestauranteId requerido");
            if (dto.NumeroMesa <= 0) throw new ArgumentException("NumeroMesa debe ser mayor a 0");
            if (dto.Capacidad <= 0) throw new ArgumentException("Capacidad debe ser mayor a 0");

            e.RestauranteId = dto.RestauranteId;
            e.Numero = dto.NumeroMesa;
            e.Capacidad = dto.Capacidad;
        }

        public async Task<IReadOnlyList<MesaDto>> GetByRestauranteAsync(Guid restauranteId, CancellationToken ct = default)
        {
            var mesas = await _mesaRepository.GetByRestauranteAsync(restauranteId, ct);
            return mesas.Select(ToDto).ToList();
        }

        public async Task<MesaDto?> GetByRestauranteAndNumeroAsync(Guid restauranteId, int numero, CancellationToken ct = default)
        {
            var mesa = await _mesaRepository.GetByRestauranteAndNumeroAsync(restauranteId, numero, ct);
            return mesa is null ? null : ToDto(mesa);
        }
    }
}