using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs.Pago;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PagosController : ControllerBase
    {
        private readonly IPagoService _pagoService;

        public PagosController(IPagoService pagoService)
        {
            _pagoService = pagoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var pagos = await _pagoService.GetAllAsync(ct);
                return Ok(pagos);
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
                var pago = await _pagoService.GetByIdAsync(id, ct);
                if (pago is null)
                    return NotFound(new { message = "Pago no encontrado" });

                return Ok(pago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("orden/{ordenId}")]
        public async Task<IActionResult> GetByOrden(Guid ordenId, CancellationToken ct = default)
        {
            try
            {
                if (ordenId == Guid.Empty)
                    return BadRequest(new { message = "OrdenId es requerido" });

                var pago = await _pagoService.GetByOrdenIdAsync(ordenId, ct);
                if (pago is null)
                    return NotFound(new { message = "Pago no encontrado para esta orden" });

                return Ok(pago);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PagoDto dto, CancellationToken ct = default)
        {
            try
            {
                var pago = await _pagoService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = pago.Id }, pago);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("procesar")]
        public async Task<IActionResult> ProcesarPago([FromBody] ProcesarPagoDto dto, CancellationToken ct = default)
        {
            try
            {
                var pagoDto = new PagoDto
                {
                    OrdenId = dto.OrdenId,
                    MetodoPago = dto.MetodoPago,
                    // Monto y estado se calculan en el servicio
                };

                var pago = await _pagoService.CreateAsync(pagoDto, ct);
                return Ok(pago);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] PagoDto dto, CancellationToken ct = default)
        {
            try
            {
                var pago = await _pagoService.UpdateAsync(id, dto, ct);
                if (pago is null)
                    return NotFound(new { message = "Pago no encontrado" });

                return Ok(pago);
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

                var result = await _pagoService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Pago no encontrado" });

                return Ok(new { message = "Pago eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}