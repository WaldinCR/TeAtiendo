using System;
using System.Threading.Tasks;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Context;
using TeAtiendo.Persistence.Repositories.Operaciones;
using TeAtiendo.Persistence.Repositories.Seguridad;

namespace TeAtiendo.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TeAtiendoContext _context;

        public IUsuarioRepository Usuarios { get; private set; }
        public IReservaRepository Reservas { get; private set; }
        public IOrdenRepository Ordenes { get; private set; }
        public IPagoRepository Pagos { get; private set; }

        public UnitOfWork(TeAtiendoContext context)
        {
            _context = context;

            // Aquí inicializarás los repositorios concretos una vez los tengamos creados
            Usuarios = new UsuarioRepository(_context);
            Reservas = new ReservaRepository(_context);
            Ordenes = new OrdenRepository(_context);
            Pagos = new PagoRepository(_context);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}