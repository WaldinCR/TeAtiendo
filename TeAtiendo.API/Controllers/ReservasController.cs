using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs.Reserva;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservasController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservasController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var reservas = await _reservaService.GetAllAsync(ct);
                return Ok(reservas);
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
                var reserva = await _reservaService.GetByIdAsync(id, ct);
                if (reserva is null)
                    return NotFound(new { message = "Reserva no encontrada" });

                return Ok(reserva);
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
                var reservas = await _reservaService.GetByUsuarioAsync(usuarioId, ct);
                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ReservaDto dto, CancellationToken ct = default)
        {
            try
            {
                var reserva = await _reservaService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = reserva.Id }, reserva);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] ReservaDto dto, CancellationToken ct = default)
        {
            try
            {
                var reserva = await _reservaService.UpdateAsync(id, dto, ct);
                if (reserva is null)
                    return NotFound(new { message = "Reserva no encontrada" });

                return Ok(reserva);
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

                var result = await _reservaService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Reserva no encontrada" });

                return Ok(new { message = "Reserva eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("{id}/cancelar")]
        public async Task<IActionResult> CancelarReserva(Guid id, [FromQuery] Guid userId, CancellationToken ct = default)
        {
            try
            {
                if (userId == Guid.Empty)
                    return BadRequest(new { message = "UserId es requerido" });

                var result = await _reservaService.CancelarReservaAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Reserva no encontrada" });

                return Ok(new { message = "Reserva cancelada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}