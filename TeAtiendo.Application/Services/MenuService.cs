using TeAtiendo.Application.DTOs.Menu;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Entities.Catalogo;
using TeAtiendo.Persistence.Interface;

namespace TeAtiendo.Application.Services
{
    public class MenuService : IMenuService
    {
        private readonly IUnitOfWork _uow;

        public MenuService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<IReadOnlyList<MenuDto>> GetAllAsync(CancellationToken ct = default)
        {
            var menus = await _uow.Menus.GetAllAsync(ct);
            return menus.Select(m => new MenuDto
            {
                Id = m.Id,
                Nombre = m.Nombre,
                Descripcion = m.Descripcion,
                RestauranteId = m.RestauranteId,
                FechaCreacion = m.FechaCreacion
            }).ToList();
        }

        public async Task<MenuDto?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            var menu = await _uow.Menus.GetByIdAsync(id, ct);
            if (menu is null) return null;

            return new MenuDto
            {
                Id = menu.Id,
                Nombre = menu.Nombre,
                Descripcion = menu.Descripcion,
                RestauranteId = menu.RestauranteId,
                FechaCreacion = menu.FechaCreacion
            };
        }

        public async Task<MenuDto> CreateAsync(CreateMenuDto dto, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre))
                throw new ArgumentException("Nombre requerido");

            var menu = new Menu
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre.Trim(),
                Descripcion = dto.Descripcion?.Trim(),
                RestauranteId = dto.RestauranteId,
                Activo = true,
                Categorias = new List<CategoriaPlato>()
            };

            await _uow.Menus.AddAsync(menu, ct);
            await _uow.SaveAsync(ct);

            return new MenuDto
            {
                Id = menu.Id,
                Nombre = menu.Nombre,
                Descripcion = menu.Descripcion,
                RestauranteId = menu.RestauranteId,
                FechaCreacion = menu.FechaCreacion
            };
        }

        public async Task<MenuDto> UpdateAsync(Guid id, UpdateMenuDto dto, CancellationToken ct = default)
        {
            var menu = await _uow.Menus.GetByIdAsync(id, ct);
            if (menu is null)
                throw new InvalidOperationException("Menú no encontrado");

            if (!string.IsNullOrWhiteSpace(dto.Nombre))
                menu.Nombre = dto.Nombre.Trim();

            if (!string.IsNullOrWhiteSpace(dto.Descripcion))
                menu.Descripcion = dto.Descripcion.Trim();

            if (dto.RestauranteId.HasValue)
                menu.RestauranteId = dto.RestauranteId.Value;

            menu.FechaModificacion = DateTime.UtcNow;

            await _uow.Menus.UpdateAsync(menu, ct);
            await _uow.SaveAsync(ct);

            return new MenuDto
            {
                Id = menu.Id,
                Nombre = menu.Nombre,
                Descripcion = menu.Descripcion,
                RestauranteId = menu.RestauranteId,
                FechaCreacion = menu.FechaCreacion
            };
        }

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var menu = await _uow.Menus.GetByIdAsync(id, ct);
            if (menu is null)
                throw new InvalidOperationException("Menú no encontrado");

            await _uow.Menus.SoftDeleteAsync(id, Guid.Empty, ct);
            await _uow.SaveAsync(ct);
        }
    }
}