using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MesasController : ControllerBase
    {
        private readonly IMesaService _mesaService;

        public MesasController(IMesaService mesaService)
        {
            _mesaService = mesaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var mesas = await _mesaService.GetAllAsync(ct);
                return Ok(mesas);
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
                var mesa = await _mesaService.GetByIdAsync(id, ct);
                if (mesa is null)
                    return NotFound(new { message = "Mesa no encontrada" });

                return Ok(mesa);
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
                var mesas = await _mesaService.GetByRestauranteAsync(restauranteId, ct);
                return Ok(mesas);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("restaurante/{restauranteId}/numero/{numero}")]
        public async Task<IActionResult> GetByRestauranteAndNumero(Guid restauranteId, int numero, CancellationToken ct = default)
        {
            try
            {
                var mesa = await _mesaService.GetByRestauranteAndNumeroAsync(restauranteId, numero, ct);
                if (mesa is null)
                    return NotFound(new { message = "Mesa no encontrada" });

                return Ok(mesa);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] MesaDto dto, CancellationToken ct = default)
        {
            try
            {
                var mesa = await _mesaService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = mesa.Id }, mesa);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] MesaDto dto, CancellationToken ct = default)
        {
            try
            {
                var mesa = await _mesaService.UpdateAsync(id, dto, ct);
                if (mesa is null)
                    return NotFound(new { message = "Mesa no encontrada" });

                return Ok(mesa);
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

                var result = await _mesaService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Mesa no encontrada" });

                return Ok(new { message = "Mesa eliminada exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}