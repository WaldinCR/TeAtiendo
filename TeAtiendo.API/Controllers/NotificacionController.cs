using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificacionesController : ControllerBase
    {
        private readonly INotificacionService _notificacionService;

        public NotificacionesController(INotificacionService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var notificaciones = await _notificacionService.GetAllAsync(ct);
                return Ok(notificaciones);
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
                var notificacion = await _notificacionService.GetByIdAsync(id, ct);
                if (notificacion is null)
                    return NotFound(new { message = "Notificación no encontrada" });

                return Ok(notificacion);
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
                var notificaciones = await _notificacionService.GetByUsuarioAsync(usuarioId, ct);
                return Ok(notificaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("usuario/{usuarioId}/noleidas")]
        public async Task<IActionResult> GetNoLeidasByUsuario(Guid usuarioId, CancellationToken ct = default)
        {
            try
            {
                var notificaciones = await _notificacionService.GetNoLeidasByUsuarioAsync(usuarioId, ct);
                return Ok(notificaciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NotificacionDto dto, CancellationToken ct = default)
        {
            try
            {
                var notificacion = await _notificacionService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = notificacion.Id }, notificacion);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] NotificacionDto dto, CancellationToken ct = default)
        {
            try
            {
                var notificacion = await _notificacionService.UpdateAsync(id, dto, ct);
                if (notificacion is null)
                    return NotFound(new { message = "Notificación no encontrada" });

                return Ok(notificacion);
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

        [HttpPatch("{id}/marcar-leida")]
        public async Task<IActionResult> MarkAsRead(Guid id, CancellationToken ct = default)
        {
            try
            {
                var result = await _notificacionService.MarkAsReadAsync(id, ct);
                if (!result)
                    return NotFound(new { message = "Notificación no encontrada" });

                return Ok(new { message = "Notificación marcada como leída" });
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

                var result = await _notificacionService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Notificación no encontrada" });

                return Ok(new { message = "Notificación eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}