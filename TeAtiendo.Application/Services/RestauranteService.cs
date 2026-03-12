using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Restaurante;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Catalog;

namespace TeAtiendo.Application.Services
{
    public sealed class RestauranteService : BaseService<Restaurante, RestauranteDto>, IRestauranteService
    {

        public RestauranteService(
            TeAtiendo.Domain.Interfaces.IRestauranteRepository repo,
            TeAtiendo.Persistence.Interface.IUnitOfWork uow)
            : base(repo, uow)
        {
        }

        protected override RestauranteDto ToDto(Restaurante e) => new()
        {
            Id = e.Id,
            Nombre = e.Nombre,
            Direccion = e.Direccion,
            Telefono = e.Telefono,
            Correo = e.Correo,
            HorarioApertura = e.HorarioApertura,
            HorarioCierre = e.HorarioCierre
        };

        protected override void ApplyDto(RestauranteDto dto, Restaurante e)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre)) throw new ArgumentException("Nombre requerido");
            if (dto.HorarioCierre <= dto.HorarioApertura) throw new ArgumentException("Horario inválido");

            e.Nombre = dto.Nombre.Trim();
            e.Direccion = (dto.Direccion ?? "").Trim();
            e.Telefono = (dto.Telefono ?? "").Trim();
            e.Correo = (dto.Correo ?? "").Trim();
            e.HorarioApertura = dto.HorarioApertura;
            e.HorarioCierre = dto.HorarioCierre;
        }

        public async Task<IReadOnlyList<RestauranteDto>> BuscarPorNombreAsync(string nombre, CancellationToken ct = default)
        {
            nombre = nombre?.Trim() ?? "";
            var list = await GetAllAsync(ct);
            if (string.IsNullOrWhiteSpace(nombre)) return list;

            return list
                .Where(r => r.Nombre.Contains(nombre, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}