using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Usuario;

namespace TeAtiendo.Application.Interfaces
{
    public interface IUsuarioService : IBaseService<UsuarioDto>
    {
        Task<UsuarioDto?> GetByEmailAsync(string correo, CancellationToken ct = default);
    }
}