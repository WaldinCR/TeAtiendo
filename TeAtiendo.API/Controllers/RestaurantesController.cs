using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs.Restaurante;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RestaurantesController : ControllerBase
    {
        private readonly IRestauranteService _restauranteService;

        public RestaurantesController(IRestauranteService restauranteService)
        {
            _restauranteService = restauranteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var restaurantes = await _restauranteService.GetAllAsync(ct);
                return Ok(restaurantes);
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
                var restaurante = await _restauranteService.GetByIdAsync(id, ct);
                if (restaurante is null)
                    return NotFound(new { message = "Restaurante no encontrado" });

                return Ok(restaurante);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorNombre([FromQuery] string nombre, CancellationToken ct = default)
        {
            try
            {
                var restaurantes = await _restauranteService.BuscarPorNombreAsync(nombre, ct);
                return Ok(restaurantes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RestauranteDto dto, CancellationToken ct = default)
        {
            try
            {
                var restaurante = await _restauranteService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = restaurante.Id }, restaurante);
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
        public async Task<IActionResult> Update(Guid id, [FromBody] RestauranteDto dto, CancellationToken ct = default)
        {
            try
            {
                var restaurante = await _restauranteService.UpdateAsync(id, dto, ct);
                if (restaurante is null)
                    return NotFound(new { message = "Restaurante no encontrado" });

                return Ok(restaurante);
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

                var result = await _restauranteService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Restaurante no encontrado" });

                return Ok(new { message = "Restaurante eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}