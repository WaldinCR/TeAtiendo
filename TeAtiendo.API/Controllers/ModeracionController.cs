using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs.Moderacion;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModeracionController : ControllerBase
    {
        private readonly IModeracionService _moderacionService;

        public ModeracionController(IModeracionService moderacionService)
        {
            _moderacionService = moderacionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var items = await _moderacionService.GetAllAsync(ct);
                return Ok(items);
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
                var item = await _moderacionService.GetByIdAsync(id, ct);
                if (item is null)
                    return NotFound(new { message = "Registro de moderación no encontrado" });

                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("admin/{adminId}")]
        public async Task<IActionResult> GetByAdmin(Guid adminId, CancellationToken ct = default)
        {
            try
            {
                var items = await _moderacionService.GetByAdminAsync(adminId, ct);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("tipo/{tipoContenido}")]
        public async Task<IActionResult> GetByTipoContenido(string tipoContenido, CancellationToken ct = default)
        {
            try
            {
                var items = await _moderacionService.GetByTipoContenidoAsync(tipoContenido, ct);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("estado/{estado}")]
        public async Task<IActionResult> GetByEstado(string estado, CancellationToken ct = default)
        {
            try
            {
                var items = await _moderacionService.GetByEstadoAsync(estado, ct);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ModeracionContenidoDto dto, CancellationToken ct = default)
        {
            try
            {
                var item = await _moderacionService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
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

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(Guid id, [FromBody] CambiarEstadoModeracionDto dto, CancellationToken ct = default)
        {
            try
            {
                var item = await _moderacionService.CambiarEstadoAsync(id, dto.NuevoEstado, ct);
                if (item is null)
                    return NotFound(new { message = "Registro de moderación no encontrado" });

                return Ok(item);
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

                var result = await _moderacionService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Registro de moderación no encontrado" });

                return Ok(new { message = "Registro eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }

    public sealed class CambiarEstadoModeracionDto
    {
        public string NuevoEstado { get; set; } = string.Empty;
    }
}