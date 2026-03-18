using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs.Restaurante;
using TeAtiendo.Application.Interfaces;
using System.ComponentModel.DataAnnotations;

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

        /// <summary>
        /// Obtiene todos los restaurantes
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var restaurantes = await _restauranteService.GetAllAsync(ct);
                return Ok(new { message = "Restaurantes obtenidos exitosamente", data = restaurantes });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message, traceId = HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Obtiene un restaurante por ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "El ID del restaurante no es válido" });

            try
            {
                var restaurante = await _restauranteService.GetByIdAsync(id, ct);
                if (restaurante is null)
                    return NotFound(new { message = "Restaurante no encontrado" });

                return Ok(new { message = "Restaurante obtenido exitosamente", data = restaurante });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message, traceId = HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Busca restaurantes por nombre
        /// </summary>
        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorNombre([FromQuery] string nombre, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return BadRequest(new { message = "El nombre del restaurante es requerido para buscar" });

            try
            {
                var restaurantes = await _restauranteService.BuscarPorNombreAsync(nombre, ct);
                return Ok(new { message = "Búsqueda realizada exitosamente", data = restaurantes, total = restaurantes.Count() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message, traceId = HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Crea un nuevo restaurante
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RestauranteDto dto, CancellationToken ct = default)
        {
            // Validar que el DTO no sea null
            if (dto is null)
                return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vacío" });

            // Validar ModelState
            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { message = "Errores de validación", errors = errores });
            }

            // Validar que los horarios sean coherentes
            if (dto.HorarioApertura >= dto.HorarioCierre)
                return BadRequest(new { message = "El horario de apertura debe ser menor al horario de cierre" });

            try
            {
                var restaurante = await _restauranteService.CreateAsync(dto, ct);
                return CreatedAtAction(nameof(GetById), new { id = restaurante.Id },
                    new { message = "Restaurante creado exitosamente", data = restaurante });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "Error de validación", error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = "Error de operación", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message, traceId = HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Actualiza un restaurante existente
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] RestauranteDto dto, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "El ID del restaurante no es válido" });

            if (dto is null)
                return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vacío" });

            if (!ModelState.IsValid)
            {
                var errores = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();
                return BadRequest(new { message = "Errores de validación", errors = errores });
            }

            if (dto.HorarioApertura >= dto.HorarioCierre)
                return BadRequest(new { message = "El horario de apertura debe ser menor al horario de cierre" });

            try
            {
                var restaurante = await _restauranteService.UpdateAsync(id, dto, ct);
                if (restaurante is null)
                    return NotFound(new { message = "Restaurante no encontrado" });

                return Ok(new { message = "Restaurante actualizado exitosamente", data = restaurante });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = "Error de validación", error = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = "Error de operación", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message, traceId = HttpContext.TraceIdentifier });
            }
        }

        /// <summary>
        /// Elimina un restaurante
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, [FromQuery] Guid userId, CancellationToken ct = default)
        {
            if (id == Guid.Empty)
                return BadRequest(new { message = "El ID del restaurante no es válido" });

            if (userId == Guid.Empty)
                return BadRequest(new { message = "El ID del usuario no es válido" });

            try
            {
                var result = await _restauranteService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Restaurante no encontrado" });

                return Ok(new { message = "Restaurante eliminado exitosamente" });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = "Error de operación", error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error interno del servidor", error = ex.Message, traceId = HttpContext.TraceIdentifier });
            }
        }
    }
}