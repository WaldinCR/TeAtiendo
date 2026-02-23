using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IPagoRepository
    {
        Task<Pago?> GetByIdAsync(int id);
        Task<IEnumerable<Pago>> GetAllAsync();
        Task AddAsync(Pago pago);
        Task UpdateAsync(Pago pago);
        Task DeleteAsync(int id);
    }
}