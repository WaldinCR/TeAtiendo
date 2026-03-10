using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TeAtiendo.Domain.Entities.Catalog;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IPlatoRepository
    {
        Task<IEnumerable<Plato>> GetAllAsync();
        Task<Plato?> GetByIdAsync(Guid id);
        Task AddAsync(Plato plato);
        Task UpdateAsync(Plato plato);
        Task DeleteAsync(Guid id);
    }
}