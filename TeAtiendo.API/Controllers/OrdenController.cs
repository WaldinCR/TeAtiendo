using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs.Orden;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdenesController : ControllerBase
    {
        private readonly IOrdenService _ordenService;

        public OrdenesController(IOrdenService ordenService)
        {
            _ordenService = ordenService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var ordenes = await _ordenService.GetAllAsync(ct);
                return Ok(ordenes);
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
                var orden = await _ordenService.GetByIdAsync(id, ct);
                if (orden is null)
                    return NotFound(new { message = "Orden no encontrada" });

                return Ok(orden);
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
                if (usuarioId == Guid.Empty)
                    return BadRequest(new { message = "UsuarioId es requerido" });

                var ordenes = await _ordenService.GetByUsuarioIdAsync(usuarioId, ct);
                return Ok(ordenes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SaveOrdenDto dto, CancellationToken ct = default)
        {
            try
            {
                var orden = await _ordenService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = orden.Id }, orden);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] OrdenDto dto, CancellationToken ct = default)
        {
            try
            {
                var orden = await _ordenService.UpdateAsync(id, dto, ct);
                if (orden is null)
                    return NotFound(new { message = "Orden no encontrada" });

                return Ok(orden);
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
        public async Task<IActionResult> CambiarEstado(Guid id, [FromBody] CambiarEstadoOrdenDto dto, CancellationToken ct = default)
        {
            try
            {
                var orden = await _ordenService.CambiarEstadoAsync(id, dto.NuevoEstado, ct);
                if (orden is null)
                    return NotFound(new { message = "Orden no encontrada" });

                return Ok(orden);
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

                var result = await _ordenService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Orden no encontrada" });

                return Ok(new { message = "Orden eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}