using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs.Auditoria;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuditoriasController : ControllerBase
    {
        private readonly IAuditoriaService _auditoriaService;

        public AuditoriasController(IAuditoriaService auditoriaService)
        {
            _auditoriaService = auditoriaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var auditorias = await _auditoriaService.GetAllAsync(ct);
                return Ok(auditorias);
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
                var auditoria = await _auditoriaService.GetByIdAsync(id, ct);
                if (auditoria is null)
                    return NotFound(new { message = "Registro de auditoría no encontrado" });

                return Ok(auditoria);
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
                var auditorias = await _auditoriaService.GetByAdminAsync(adminId, ct);
                return Ok(auditorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("modulo/{modulo}")]
        public async Task<IActionResult> GetByModulo(string modulo, CancellationToken ct = default)
        {
            try
            {
                var auditorias = await _auditoriaService.GetByModuloAsync(modulo, ct);
                return Ok(auditorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("rango")]
        public async Task<IActionResult> GetByFechaRango([FromQuery] DateTime desde, [FromQuery] DateTime hasta, CancellationToken ct = default)
        {
            try
            {
                if (desde > hasta)
                    return BadRequest(new { message = "La fecha 'desde' debe ser menor o igual a 'hasta'" });

                var auditorias = await _auditoriaService.GetByFechaRangoAsync(desde, hasta, ct);
                return Ok(auditorias);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Registrar([FromBody] AuditoriaDto dto, CancellationToken ct = default)
        {
            try
            {
                var auditoria = await _auditoriaService.RegistrarAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = auditoria.Id }, auditoria);
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
    }
}