using TeAtiendo.Domain.Interfaces;

namespace TeAtiendo.Persistence.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository Usuarios { get; }
        IReservaRepository Reservas { get; }
        IOrdenRepository Ordenes { get; }
        IPagoRepository Pagos { get; }
        IAuditoriaRepository Auditorias { get; }
        IMenuRepository Menus { get; }

        Task<int> SaveAsync(CancellationToken ct = default);
    }
}