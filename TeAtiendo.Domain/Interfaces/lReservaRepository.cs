using TeAtiendo.Domain.Entities.Operaciones;

namespace TeAtiendo.Domain.Interfaces;

public interface IReservaRepository
{
    Task<Reserva?> GetByIdAsync(int id);
    Task<IEnumerable<Reserva>> GetAllAsync();
    Task AddAsync(Reserva reserva);
    Task UpdateAsync(Reserva reserva);
    Task DeleteAsync(int id);
}
