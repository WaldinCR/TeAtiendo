using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IOrdenRepository
    {
        Task<Orden?> GetByIdAsync(int id);

        Task<IEnumerable<Orden>> GetAllAsync();

        Task AddAsync(Orden orden);

        Task UpdateAsync(Orden orden);

        Task DeleteAsync(int id);
    }
}