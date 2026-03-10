using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IOrdenRepository
    {
        Task<IEnumerable<Orden>> GetAllAsync();
        Task<Orden?> GetByIdAsync(Guid id);
        Task<IEnumerable<Orden>> GetByUsuarioAsync(Guid usuarioId);
        Task AddAsync(Orden orden);
        Task UpdateAsync(Orden orden);
        Task DeleteAsync(Guid id);
    }
}