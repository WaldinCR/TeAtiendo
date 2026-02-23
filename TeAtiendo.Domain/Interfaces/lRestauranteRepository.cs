using TeAtiendo.Domain.Entities.Catalog;

namespace TeAtiendo.Domain.Interfaces;

public interface IRestauranteRepository
{
    Task<Restaurante?> GetByIdAsync(int id);
    Task<IEnumerable<Restaurante>> GetAllAsync();
    Task AddAsync(Restaurante restaurante);
    Task UpdateAsync(Restaurante restaurante);
    Task DeleteAsync(int id);
}
