using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Interface;

namespace TeAtiendo.Application.Services
{
    public sealed class CategoriaplatoService : BaseService<CategoriaPlato, CategoriaplatoDto>, ICategoriaplatoService
    {
        private readonly ICategoriaplatoRepository _categoriaplatoRepository;

        public CategoriaplatoService(IUnitOfWork uow, ICategoriaplatoRepository categoriaplatoRepository)
            : base(categoriaplatoRepository, uow)
        {
            _categoriaplatoRepository = categoriaplatoRepository;
        }

        protected override CategoriaplatoDto ToDto(CategoriaPlato e) => new()
        {
            Id = e.Id,
            MenuId = e.MenuId,
            Nombre = e.Nombre,
            Descripcion = e.Descripcion,
            FechaCreacion = e.CreationDate
        };

        protected override void ApplyDto(CategoriaplatoDto dto, CategoriaPlato e)
        {
            if (dto.MenuId == Guid.Empty) throw new ArgumentException("MenuId requerido");
            if (string.IsNullOrWhiteSpace(dto.Nombre)) throw new ArgumentException("Nombre requerido");

            e.MenuId = dto.MenuId;
            e.Nombre = dto.Nombre.Trim();
            e.Descripcion = dto.Descripcion?.Trim() ?? string.Empty;
        }

        public async Task<IReadOnlyList<CategoriaplatoDto>> GetByMenuAsync(Guid menuId, CancellationToken ct = default)
        {
            var categorias = await _categoriaplatoRepository.GetByMenuAsync(menuId, ct);
            return categorias.Select(ToDto).ToList();
        }
    }
}