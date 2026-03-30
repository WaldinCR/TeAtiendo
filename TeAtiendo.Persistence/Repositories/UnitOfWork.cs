using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Context;
using TeAtiendo.Persistence.Interface;

namespace TeAtiendo.Persistence.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly TeAtiendoContext _context;

        public IUsuarioRepository Usuarios { get; }
        public IRestauranteRepository Restaurantes { get; }
        public IReservaRepository Reservas { get; }
        public IOrdenRepository Ordenes { get; }
        public IPagoRepository Pagos { get; }
        public IAuditoriaRepository Auditorias { get; }
        public IMenuRepository Menus { get; }
        public IMesaRepository Mesas { get; }
        public IDisponibilidadRepository Disponibilidades { get; }
        public IModeracionContenidoRepository ModeracionContenidos { get; }

        public UnitOfWork(
            TeAtiendoContext context,
            IUsuarioRepository usuarios,
            IRestauranteRepository restaurantes,
            IReservaRepository reservas,
            IOrdenRepository ordenes,
            IPagoRepository pagos,
            IAuditoriaRepository auditorias,
            IMenuRepository menus,
            IMesaRepository mesas,
            IDisponibilidadRepository disponibilidades,
            IModeracionContenidoRepository moderacionContenidos)
        {
            _context = context;
            Usuarios = usuarios;
            Restaurantes = restaurantes;
            Reservas = reservas;
            Ordenes = ordenes;
            Pagos = pagos;
            Auditorias = auditorias;
            Menus = menus;
            Mesas = mesas;
            Disponibilidades = disponibilidades;
            ModeracionContenidos = moderacionContenidos;
        }

        public Task<int> SaveAsync(CancellationToken ct = default)
            => _context.SaveChangesAsync(ct);

        public void Dispose() => _context.Dispose();
    }
}