using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Interface;

namespace TeAtiendo.Application.Services
{
    public sealed class PlatoService : BaseService<Plato, PlatoDto>, IPlatoService
    {
        private readonly IPlatoRepository _platoRepository;

        public PlatoService(IUnitOfWork uow, IPlatoRepository platoRepository)
            : base(platoRepository, uow)
        {
            _platoRepository = platoRepository;
        }

        protected override PlatoDto ToDto(Plato e) => new()
        {
            Id = e.Id,
            CategoriaPlatoId = e.CategoriaPlatoId,
            MenuId = e.MenuId,
            Nombre = e.Nombre,
            Descripcion = e.Descripcion,
            Precio = e.Precio,
            Disponible = true,
            FechaCreacion = e.CreationDate,
            Activo = e.Activo
        };

        protected override void ApplyDto(PlatoDto dto, Plato e)
        {
            if (dto.CategoriaPlatoId == Guid.Empty) throw new ArgumentException("CategoriaPlatoId requerido");
            if (dto.MenuId == Guid.Empty) throw new ArgumentException("MenuId requerido");
            if (string.IsNullOrWhiteSpace(dto.Nombre)) throw new ArgumentException("Nombre requerido");
            if (dto.Precio <= 0) throw new ArgumentException("Precio debe ser mayor a 0");

            e.CategoriaPlatoId = dto.CategoriaPlatoId;
            e.MenuId = dto.MenuId;
            e.Nombre = dto.Nombre.Trim();
            e.Descripcion = dto.Descripcion?.Trim() ?? string.Empty;
            e.Precio = dto.Precio;
        }

        public async Task<IReadOnlyList<PlatoDto>> GetByCategoriaAsync(Guid categoriaId, CancellationToken ct = default)
        {
            var platos = await _platoRepository.GetByCategoriaAsync(categoriaId, ct);
            return platos.Select(ToDto).ToList();
        }

        public async Task<IReadOnlyList<PlatoDto>> ObtenerPorMenuAsync(Guid menuId, CancellationToken ct = default)
        {
            var platos = await _platoRepository.ObtenerPorMenuAsync(menuId, ct);
            return platos.Select(ToDto).ToList();
        }
    }
}