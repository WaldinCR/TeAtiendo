using System.Windows.Controls;
using TeAtiendo.Domain.Entities.Operaciones;

namespace TeAtiendo.Domain.Interfaces;

public interface IOrdenRepository
{
    Task<Orden?> GetByIdAsync(int id);
    Task<IEnumerable<Orden>> GetAllAsync();
    Task AddAsync(Orden orden);
    Task UpdateAsync(Orden orden);
    Task DeleteAsync(int id);
}
