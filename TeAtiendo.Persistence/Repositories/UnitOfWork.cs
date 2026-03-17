using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Context;
using TeAtiendo.Persistence.Interface;

namespace TeAtiendo.Persistence.Repositories
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly TeAtiendoContext _context;

        public IUsuarioRepository Usuarios { get; }
        public IReservaRepository Reservas { get; }
        public IOrdenRepository Ordenes { get; }
        public IPagoRepository Pagos { get; }
        public IAuditoriaRepository Auditorias { get; }
        public IMenuRepository Menus { get; }

        public UnitOfWork(
            TeAtiendoContext context,
            IUsuarioRepository usuarios,
            IReservaRepository reservas,
            IOrdenRepository ordenes,
            IPagoRepository pagos,
            IAuditoriaRepository auditorias,
            IMenuRepository menus)
        {
            _context = context;
            Usuarios = usuarios;
            Reservas = reservas;
            Ordenes = ordenes;
            Pagos = pagos;
            Auditorias = auditorias;
            Menus = menus;
        }

        public Task<int> SaveAsync(CancellationToken ct = default)
            => _context.SaveChangesAsync(ct);

        public void Dispose() => _context.Dispose();
    }
}