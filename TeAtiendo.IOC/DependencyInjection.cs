using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Application.Services;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Context;
using TeAtiendo.Persistence.Interface;
using TeAtiendo.Persistence.Repositories;
using TeAtiendo.Persistence.Repositories.Auditory;
using TeAtiendo.Persistence.Repositories.Catalog;
using TeAtiendo.Persistence.Repositories.Operaciones;
using TeAtiendo.Persistence.Repositories.Seguridad;
using TeAtiendo.Persistence.Repositories.Social;

namespace TeAtiendo.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddTeAtiendoDependencies(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services
                .AddDatabase(configuration)
                .AddRepositories()
                .AddServices()
                .AddJwtAuthentication(configuration);

            return services;
        }

        // ── Base de datos ────────────────────────────────────────────────
        private static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<TeAtiendoContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        // ── Repositorios ─────────────────────────────────────────────────
        private static IServiceCollection AddRepositories(
            this IServiceCollection services)
        {
            // Seguridad
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            // Catálogo
            services.AddScoped<IRestauranteRepository, RestauranteRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<ICategoriaplatoRepository, CategoriaplatoRepository>();
            services.AddScoped<IPlatoRepository, PlatoRepository>();

            // Operaciones
            services.AddScoped<IMesaRepository, MesaRepository>();
            services.AddScoped<IDisponibilidadRepository, DisponibilidadRepository>();
            services.AddScoped<IReservaRepository, ReservaRepository>();
            services.AddScoped<IOrdenRepository, OrdenRepository>();
            services.AddScoped<IPagoRepository, PagoRepository>();

            // Social
            services.AddScoped<IResenaRepository, ResenaRepository>();
            services.AddScoped<INotificacionRepository, NotificacionRepository>();

            // Auditoría
            services.AddScoped<IAuditoriaRepository, AuditoriaRepository>();

            return services;
        }

        // ── Servicios Application ─────────────────────────────────────────
        private static IServiceCollection AddServices(
            this IServiceCollection services)
        {
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IRestauranteService, RestauranteService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<ICategoriaplatoService, CategoriaplatoService>();
            services.AddScoped<IPlatoService, PlatoService>();
            services.AddScoped<IMesaService, MesaService>();
            services.AddScoped<IDisponibilidadService, DisponibilidadService>();
            services.AddScoped<IReservaService, ReservaService>();
            services.AddScoped<IOrdenService, OrdenService>();
            services.AddScoped<IPagoService, PagoService>();
            services.AddScoped<IResenaService, ResenaService>();
            services.AddScoped<INotificacionService, NotificacionService>();

            return services;
        }

        // ── JWT ───────────────────────────────────────────────────────────
        private static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = Encoding.UTF8.GetBytes(
                jwtSettings["Secret"] ?? throw new InvalidOperationException("JWT Secret not configured"));

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(secretKey),
                        ValidateIssuer = true,
                        ValidIssuer = jwtSettings["Issuer"],
                        ValidateAudience = true,
                        ValidAudience = jwtSettings["Audience"],
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero
                    };
                });

            services.AddAuthorization();

            return services;
        }
    }
}