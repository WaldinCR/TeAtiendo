using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Social;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Interface;

namespace TeAtiendo.Application.Services
{
    public sealed class ResenaService : BaseService<Resena, ResenaDto>, IResenaService
    {
        private readonly IResenaRepository _resenaRepository;

        public ResenaService(IUnitOfWork uow, IResenaRepository resenaRepository)
            : base(resenaRepository, uow)
        {
            _resenaRepository = resenaRepository;
        }

        protected override ResenaDto ToDto(Resena e) => new()
        {
            Id = e.Id,
            UsuarioId = e.UsuarioId,
            RestauranteId = e.RestauranteId,
            ReservaId = e.ReservaId,
            OrdenId = e.OrdenId,
            Calificacion = e.Calificacion,
            Comentario = e.Comentario,
            Fecha = e.Fecha,
            Estado = e.Estado,
            FechaCreacion = e.CreationDate
        };

        protected override void ApplyDto(ResenaDto dto, Resena e)
        {
            if (dto.UsuarioId == Guid.Empty) throw new ArgumentException("UsuarioId requerido");
            if (dto.RestauranteId == Guid.Empty) throw new ArgumentException("RestauranteId requerido");
            if (dto.Calificacion < 1 || dto.Calificacion > 5) throw new ArgumentException("Calificacion debe estar entre 1 y 5");

            e.UsuarioId = dto.UsuarioId;
            e.RestauranteId = dto.RestauranteId;
            e.ReservaId = dto.ReservaId;
            e.OrdenId = dto.OrdenId;
            e.Calificacion = dto.Calificacion;
            e.Comentario = dto.Comentario?.Trim() ?? string.Empty;
            e.Fecha = dto.Fecha == default ? DateTime.UtcNow : dto.Fecha;
            e.Estado = dto.Estado;
        }

        public async Task<IReadOnlyList<ResenaDto>> GetByRestauranteAsync(Guid restauranteId, CancellationToken ct = default)
        {
            var resenas = await _resenaRepository.GetByRestauranteAsync(restauranteId, ct);
            return resenas.Select(ToDto).ToList();
        }

        public async Task<IReadOnlyList<ResenaDto>> GetByUsuarioAsync(Guid usuarioId, CancellationToken ct = default)
        {
            var resenas = await _resenaRepository.GetByUsuarioAsync(usuarioId, ct);
            return resenas.Select(ToDto).ToList();
        }

        public async Task<double> GetPromediaCalificacionAsync(Guid restauranteId, CancellationToken ct = default)
        {
            return await _resenaRepository.GetPromediaCalificacionAsync(restauranteId, ct);
        }
    }
}