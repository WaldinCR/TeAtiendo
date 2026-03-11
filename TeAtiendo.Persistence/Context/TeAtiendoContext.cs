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
        public TeAtiendoContext(DbContextOptions<TeAtiendoContext> options) : base(options)
        {
        }

        #region SEGURITY
        public DbSet<Usuario> Usuarios { get; set; }
        #endregion

        #region CATALOG
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<CategoriaPlato> CategoriaPlatos { get; set; }
        public DbSet<Plato> Platos { get; set; }
        #endregion

        #region OPERATIONS
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<OrdenDetalle> OrdenDetalles { get; set; }
        public DbSet<Pago> Pagos { get; set; }
        public DbSet<Disponibilidad> Disponibilidades { get; set; }
        public DbSet<Mesa> Mesas { get; set; }
        #endregion

        #region SOCIAL
        public DbSet<Notificacion> Notificaciones { get; set; }
        public DbSet<Resena> Resenas { get; set; }
        #endregion

        #region AUDITORY
        public DbSet<Auditoria> Auditorias { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region RELACIONES DEL CATALOGO
            modelBuilder.Entity<Menu>()
                .HasOne(m => m.Restaurante)
                .WithMany(r => r.Menus)
                .HasForeignKey(m => m.RestauranteId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CategoriaPlato>()
                .HasOne(c => c.Menu)
                .WithMany(m => m.Categorias)
                .HasForeignKey(c => c.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Plato>()
                .HasOne(p => p.CategoriaPlato)
                .WithMany(c => c.Platos)
                .HasForeignKey(p => p.CategoriaPlatoId)
                .OnDelete(DeleteBehavior.Cascade);

            // Recomendado: relación explícita Plato -> Menu (tienes MenuId en la entidad)
            modelBuilder.Entity<Plato>()
                .HasOne(p => p.Menu)
                .WithMany(m => m.Categorias.SelectMany(c => c.Platos)) // si te da problemas, se quita; EF puede inferir sin esto
                .HasForeignKey(p => p.MenuId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region CONFIGURACION DECIMALES
            modelBuilder.Entity<Plato>()
                .Property(p => p.Precio)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Orden>()
                .Property(o => o.Total)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrdenDetalle>()
                .Property(od => od.PrecioUnitario)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Pago>()
                .Property(p => p.Monto)
                .HasPrecision(18, 2);
            #endregion

            #region NOMBRES DE TABLAS
            modelBuilder.Entity<Restaurante>().ToTable("Restaurantes");
            modelBuilder.Entity<Menu>().ToTable("Menus");
            modelBuilder.Entity<CategoriaPlato>().ToTable("CategoriasPlato");
            modelBuilder.Entity<Plato>().ToTable("Platos");
            #endregion
        }
    }
}