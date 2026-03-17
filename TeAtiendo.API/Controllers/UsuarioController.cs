using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs.Usuario;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken ct = default)
        {
            try
            {
                var usuarios = await _usuarioService.GetAllAsync(ct);
                return Ok(usuarios);
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
                var usuario = await _usuarioService.GetByIdAsync(id, ct);
                if (usuario is null)
                    return NotFound(new { message = "Usuario no encontrado" });

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("email/{correo}")]
        public async Task<IActionResult> GetByEmail(string correo, CancellationToken ct = default)
        {
            try
            {
                var usuario = await _usuarioService.GetByEmailAsync(correo, ct);
                if (usuario is null)
                    return NotFound(new { message = "Usuario no encontrado" });

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UsuarioDto dto, CancellationToken ct = default)
        {
            try
            {
                var usuario = await _usuarioService.UpdateAsync(id, dto, ct);
                if (usuario is null)
                    return NotFound(new { message = "Usuario no encontrado" });

                return Ok(usuario);
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

                var result = await _usuarioService.DeleteAsync(id, userId, ct);
                if (!result)
                    return NotFound(new { message = "Usuario no encontrado" });

                return Ok(new { message = "Usuario eliminado exitosamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}