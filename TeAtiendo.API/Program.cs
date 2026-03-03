using Microsoft.EntityFrameworkCore;
using TeAtiendo.Persistence.Context;
using TeAtiendo.Persistence.Repositories.Catalog;
using TeAtiendo.Persistence.Repositories.Operaciones;
using TeAtiendo.Persistence.Repositories.Auditoria;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DbContext
builder.Services.AddDbContext<TeAtiendoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Repositorios
builder.Services.AddScoped<MenuRepository>();
builder.Services.AddScoped<PlatoRepository>();
builder.Services.AddScoped<RestauranteRepository>();
builder.Services.AddScoped<OrdenRepository>();
builder.Services.AddScoped<PagoRepository>();
builder.Services.AddScoped<ReservaRepository>();
builder.Services.AddScoped<AuditoriaLogRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();