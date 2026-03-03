using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Segurity;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> GetByIdAsync(Guid id);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(Guid id);
    }
}