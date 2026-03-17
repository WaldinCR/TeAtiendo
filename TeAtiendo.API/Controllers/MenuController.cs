using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs.Menu;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenusController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenusController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MenuDto>>> GetAll(CancellationToken ct)
        {
            var menus = await _menuService.GetAllAsync(ct);
            return Ok(menus);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDto>> GetById(Guid id, CancellationToken ct)
        {
            var menu = await _menuService.GetByIdAsync(id, ct);
            if (menu is null)
                return NotFound("Menú no encontrado");
            return Ok(menu);
        }

        [HttpPost]
        public async Task<ActionResult<MenuDto>> Create(CreateMenuDto dto, CancellationToken ct)
        {
            var menu = await _menuService.CreateAsync(dto, ct);
            return CreatedAtAction(nameof(GetById), new { id = menu.Id }, menu);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MenuDto>> Update(Guid id, UpdateMenuDto dto, CancellationToken ct)
        {
            var menu = await _menuService.UpdateAsync(id, dto, ct);
            return Ok(menu);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _menuService.DeleteAsync(id, ct);
            return NoContent();
        }
    }
}