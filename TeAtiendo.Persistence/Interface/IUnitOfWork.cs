using System;
using System.Threading.Tasks;
using TeAtiendo.Domain.Interfaces;

namespace TeAtiendo.Persistence.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository Usuarios { get; }
        IReservaRepository Reservas { get; }
        IOrdenRepository Ordenes { get; }
        IPagoRepository Pagos { get; }

        Task<int> SaveAsync();
    }
}