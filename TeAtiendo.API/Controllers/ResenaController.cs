using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResenasController : ControllerBase
    {
        private readonly IResenaService _resenaService;

        public ResenasController(IResenaService resenaService)
        {
            _resenaService = resenaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var resenas = await _resenaService.GetAllAsync(ct);
                return Ok(resenas);
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
                var resena = await _resenaService.GetByIdAsync(id, ct);
                if (resena is null)
                    return NotFound(new { message = "Reseña no encontrada" });

                return Ok(resena);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("restaurante/{restauranteId}")]
        public async Task<IActionResult> GetByRestaurante(Guid restauranteId, CancellationToken ct = default)
        {
            try
            {
                var resenas = await _resenaService.GetByRestauranteAsync(restauranteId, ct);
                return Ok(resenas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("usuario/{usuarioId}")]
        public async Task<IActionResult> GetByUsuario(Guid usuarioId, CancellationToken ct = default)
        {
            try
            {
                var resenas = await _resenaService.GetByUsuarioAsync(usuarioId, ct);
                return Ok(resenas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("restaurante/{restauranteId}/promedio")]
        public async Task<IActionResult> GetPromediaCalificacion(Guid restauranteId, CancellationToken ct = default)
        {
            try
            {
                var promedio = await _resenaService.GetPromediaCalificacionAsync(restauranteId, ct);
                return Ok(new { promedio = promedio });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ResenaDto dto, CancellationToken ct = default)
        {
            try
            {
                var resena = await _resenaService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = resena.Id }, resena);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] ResenaDto dto, CancellationToken ct = default)
        {
            try
            {
                var resena = await _resenaService.UpdateAsync(id, dto, ct);
                if (resena is null)
                    return NotFound(new { message = "Reseña no encontrada" });

                return Ok(resena);
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

                var result = await _resenaService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Reseña no encontrada" });

                return Ok(new { message = "Reseña eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}