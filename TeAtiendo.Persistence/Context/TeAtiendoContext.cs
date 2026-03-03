using Microsoft.EntityFrameworkCore;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Domain.Entities.Social;
using TeAtiendo.Domain.Entities.Auditory;

namespace TeAtiendo.Persistence.Context
{
    public class TeAtiendoContext : DbContext
    {
        public TeAtiendoContext(DbContextOptions<TeAtiendoContext> options) : base(options) { }

        // Segurity
        public DbSet<Usuario> Usuarios { get; set; }

        // Catalog
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<CategoriaPlato> CategoriaPlatos { get; set; }
        public DbSet<Plato> Platos { get; set; }

        // Operations
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenDetalle> OrdenDetalles { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Disponibilidad> Disponibilidades { get; set; }
        public DbSet<Mesa> Mesas { get; set; }

        // Social
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Resena> Resenas { get; set; }

        // Auditory
        public DbSet<Auditoria> Auditorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de precisión para decimales
            modelBuilder.Entity<Plato>().Property(p => p.Precio).HasPrecision(18, 2);
            modelBuilder.Entity<Orden>().Property(o => o.Total).HasPrecision(18, 2);
            modelBuilder.Entity<OrdenDetalle>().Property(od => od.PrecioUnitario).HasPrecision(18, 2);
            modelBuilder.Entity<Pago>().Property(p => p.Monto).HasPrecision(18, 2);
        }
    }
}