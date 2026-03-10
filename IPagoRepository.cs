using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Domain.Interfaces.Operations
{
    public interface IPagoRepository
    {
        Task<Pago?> GetByIdAsync(Guid id);
        Task<IEnumerable<Pago>> GetAllAsync();
        Task<Pago?> GetByOrdenIdAsync(Guid ordenId);
        Task AddAsync(Pago pago);
        Task UpdateAsync(Pago pago);
        Task DeleteAsync(Guid id);
    }
}   