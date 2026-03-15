using Microsoft.AspNetCore.Mvc;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.DTOs.Auth;
using TeAtiendo.Application.Interfaces;

namespace TeAtiendo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto, CancellationToken ct = default)
        {
            try
            {
                var result = await _authService.RegisterAsync(dto, ct);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto, CancellationToken ct = default)
        {
            try
            {
                var result = await _authService.LoginAsync(dto, ct);
                if (!result.Success)
                    return Unauthorized(new { message = result.Message });

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto, CancellationToken ct = default)
        {
            try
            {
                var result = await _authService.ChangePasswordAsync(dto.UsuarioId, dto.PasswordActual, dto.PasswordNueva, ct);
                if (!result)
                    return BadRequest(new { message = "No se pudo cambiar la contraseña" });

                return Ok(new { message = "Contraseña cambiada exitosamente" });
            }
            catch (InvalidOperationException ex)
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