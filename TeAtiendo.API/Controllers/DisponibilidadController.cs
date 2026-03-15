using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DisponibilidadesController : ControllerBase
    {
        private readonly IDisponibilidadService _disponibilidadService;

        public DisponibilidadesController(IDisponibilidadService disponibilidadService)
        {
            _disponibilidadService = disponibilidadService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var disponibilidades = await _disponibilidadService.GetAllAsync(ct);
                return Ok(disponibilidades);
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
                var disponibilidad = await _disponibilidadService.GetByIdAsync(id, ct);
                if (disponibilidad is null)
                    return NotFound(new { message = "Disponibilidad no encontrada" });

                return Ok(disponibilidad);
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
                var disponibilidades = await _disponibilidadService.GetByRestauranteAsync(restauranteId, ct);
                return Ok(disponibilidades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("fecha")]
        public async Task<IActionResult> GetByFecha([FromQuery] DateTime fecha, CancellationToken ct = default)
        {
            try
            {
                var disponibilidades = await _disponibilidadService.GetByFechaAsync(fecha, ct);
                return Ok(disponibilidades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("restaurante/{restauranteId}/fecha")]
        public async Task<IActionResult> GetByRestauranteAndFecha(Guid restauranteId, [FromQuery] DateTime fecha, CancellationToken ct = default)
        {
            try
            {
                var disponibilidades = await _disponibilidadService.GetByRestauranteAndFechaAsync(restauranteId, fecha, ct);
                return Ok(disponibilidades);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DisponibilidadDto dto, CancellationToken ct = default)
        {
            try
            {
                var disponibilidad = await _disponibilidadService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = disponibilidad.Id }, disponibilidad);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] DisponibilidadDto dto, CancellationToken ct = default)
        {
            try
            {
                var disponibilidad = await _disponibilidadService.UpdateAsync(id, dto, ct);
                if (disponibilidad is null)
                    return NotFound(new { message = "Disponibilidad no encontrada" });

                return Ok(disponibilidad);
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

                var result = await _disponibilidadService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Disponibilidad no encontrada" });

                return Ok(new { message = "Disponibilidad eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}