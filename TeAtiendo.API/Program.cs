using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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
using TeAtiendo.IOC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TeAtiendo API",
        Version = "v1",
        Description = "API para sistema de reservas y órdenes de restaurante"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Ingresa el token JWT en el formato: Bearer {token}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddDbContext<TeAtiendoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// ===== REPOSITORIOS DOMAIN =====
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IRestauranteRepository, RestauranteRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<ICategoriaplatoRepository, CategoriaplatoRepository>();
builder.Services.AddScoped<IPlatoRepository, PlatoRepository>();
builder.Services.AddScoped<IMesaRepository, MesaRepository>();
builder.Services.AddScoped<IDisponibilidadRepository, DisponibilidadRepository>();
builder.Services.AddScoped<IReservaRepository, ReservaRepository>();
builder.Services.AddScoped<IOrdenRepository, OrdenRepository>();
builder.Services.AddScoped<IPagoRepository, PagoRepository>();
builder.Services.AddScoped<IResenaRepository, ResenaRepository>();
builder.Services.AddScoped<INotificacionRepository, NotificacionRepository>();
builder.Services.AddScoped<IAuditoriaRepository, AuditoriaRepository>();

// ===== SERVICIOS APPLICATION =====
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IRestauranteService, RestauranteService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<ICategoriaplatoService, CategoriaplatoService>();
builder.Services.AddScoped<IPlatoService, PlatoService>();
builder.Services.AddScoped<IMesaService, MesaService>();
builder.Services.AddScoped<IDisponibilidadService, DisponibilidadService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IOrdenService, OrdenService>();
builder.Services.AddScoped<IPagoService, PagoService>();
builder.Services.AddScoped<IResenaService, ResenaService>();
builder.Services.AddScoped<INotificacionService, NotificacionService>();

// ===== IOC - DEPENDENCIAS ADICIONALES =====
builder.Services.AddTeAtiendoDependencies(builder.Configuration);


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });

    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins(
                "http://localhost:3000",
                "http://localhost:4200",
                "https://yourdomain.com")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TeAtiendo API v1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();