using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IPagoRepository : IRepository<Pago>
    {
        Task<Pago?> GetByOrdenIdAsync(Guid ordenId, CancellationToken ct = default);
    }
}