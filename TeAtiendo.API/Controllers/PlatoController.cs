using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatosController : ControllerBase
    {
        private readonly IPlatoService _platoService;

        public PlatosController(IPlatoService platoService)
        {
            _platoService = platoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var platos = await _platoService.GetAllAsync(ct);
                return Ok(platos);
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
                var plato = await _platoService.GetByIdAsync(id, ct);
                if (plato is null)
                    return NotFound(new { message = "Plato no encontrado" });

                return Ok(plato);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<IActionResult> GetByCategoria(Guid categoriaId, CancellationToken ct = default)
        {
            try
            {
                var platos = await _platoService.GetByCategoriaAsync(categoriaId, ct);
                return Ok(platos);
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
                var platos = await _platoService.ObtenerPorMenuAsync(menuId, ct);
                return Ok(platos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PlatoDto dto, CancellationToken ct = default)
        {
            try
            {
                var plato = await _platoService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = plato.Id }, plato);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] PlatoDto dto, CancellationToken ct = default)
        {
            try
            {
                var plato = await _platoService.UpdateAsync(id, dto, ct);
                if (plato is null)
                    return NotFound(new { message = "Plato no encontrado" });

                return Ok(plato);
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

                var result = await _platoService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Plato no encontrado" });

                return Ok(new { message = "Plato eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}