using System.Windows.Controls;
using TeAtiendo.Domain.Entities.Operaciones;

namespace TeAtiendo.Domain.Interfaces;

public interface IPagoRepository
{
    Task<Pago?> GetByIdAsync(int id);
    Task<IEnumerable<Pago>> GetAllAsync();
    Task AddAsync(Pago pago);
    Task UpdateAsync(Pago pago);
    Task DeleteAsync(int id);
}
