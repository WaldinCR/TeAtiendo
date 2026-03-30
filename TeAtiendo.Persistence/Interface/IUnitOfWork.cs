using TeAtiendo.Domain.Interfaces;

namespace TeAtiendo.Persistence.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IUsuarioRepository Usuarios { get; }
        IRestauranteRepository Restaurantes { get; }
        IReservaRepository Reservas { get; }
        IOrdenRepository Ordenes { get; }
        IPagoRepository Pagos { get; }
        IAuditoriaRepository Auditorias { get; }
        IMenuRepository Menus { get; }
        IMesaRepository Mesas { get; }
        IDisponibilidadRepository Disponibilidades { get; }
        IModeracionContenidoRepository ModeracionContenidos { get; }

        Task<int> SaveAsync(CancellationToken ct = default);
    }
}