using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasplatoController : ControllerBase
    {
        private readonly ICategoriaplatoService _categoriaplatoService;

        public CategoriasplatoController(ICategoriaplatoService categoriaplatoService)
        {
            _categoriaplatoService = categoriaplatoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var categorias = await _categoriaplatoService.GetAllAsync(ct);
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
        {
            try
            {
                var categoria = await _categoriaplatoService.GetByIdAsync(id, ct);
                if (categoria is null)
                    return NotFound(new { message = "Categoría no encontrada" });

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("menu/{menuId}")]
        public async Task<IActionResult> GetByMenu(Guid menuId, CancellationToken ct = default)
        {
            try
            {
                var categorias = await _categoriaplatoService.GetByMenuAsync(menuId, ct);
                return Ok(categorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoriaplatoDto dto, CancellationToken ct = default)
        {
            try
            {
                var categoria = await _categoriaplatoService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] CategoriaplatoDto dto, CancellationToken ct = default)
        {
            try
            {
                var categoria = await _categoriaplatoService.UpdateAsync(id, dto, ct);
                if (categoria is null)
                    return NotFound(new { message = "Categoría no encontrada" });

                return Ok(categoria);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] Guid userId, CancellationToken ct = default)
        {
            try
            {
                if (userId == Guid.Empty)
                    return BadRequest(new { message = "UserId es requerido" });

                var result = await _categoriaplatoService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Categoría no encontrada" });

                return Ok(new { message = "Categoría eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}