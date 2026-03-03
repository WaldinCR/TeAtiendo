
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TeAtiendo.Domain.Entities.Catalog;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Entities.Segurity;

namespace TeAtiendo.Persistence.Context
{
    public class TeAtiendoContext : DbContext
    {
        public TeAtiendoContext(DbContextOptions<TeAtiendoContext> options) : base(options) { }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Restaurante> Restaurantes { get; set; }
        public DbSet<Reserva> Reservas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Validaciones de base de datos (Fluent API)
            modelBuilder.Entity<Usuario>().HasIndex(u => u.Correo).IsUnique();
        }
    }
}