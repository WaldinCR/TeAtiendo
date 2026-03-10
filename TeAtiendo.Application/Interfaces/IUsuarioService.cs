using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs;
using TeAtiendo.Application.DTOs.Usuario;

namespace TeAtiendo.Application.Interfaces
{
    public interface IUsuarioService : IBaseService<UsuarioDto, UsuarioDto, UsuarioDto>
    {
        Task<UsuarioDto?> GetByEmailAsync(string email);

        Task<IEnumerable<UsuarioDto>> GetUsuariosActivosAsync();

        Task<bool> CambiarEstadoAsync(Guid id, bool activo);
    }
}