using TeAtiendo.Application.DTOs.Auth;
using TeAtiendo.Application.DTOs.Usuario;

namespace TeAtiendo.Application.Interfaces
{
    public interface IAuthService
    {
        Task<UsuarioDto> RegisterAsync(RegisterDto dto, CancellationToken ct = default);
        Task<LoginResultDto> LoginAsync(LoginDto dto, CancellationToken ct = default);
        Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken ct = default);
    }
}