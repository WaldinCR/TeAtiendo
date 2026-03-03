using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Catalog;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IRestauranteRepository
    {
        Task<Restaurante?> GetByIdAsync(Guid id);
        Task<IEnumerable<Restaurante>> GetAllAsync();
        Task AddAsync(Restaurante restaurante);
        Task UpdateAsync(Restaurante restaurante);
        Task DeleteAsync(Guid id);
    }
}