using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Orden;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Application.Services
{
    public sealed class OrdenService : BaseService<Orden, OrdenDto>, IOrdenService
    {
        public OrdenService(TeAtiendo.Persistence.Interface.IUnitOfWork uow)
            : base(uow.Ordenes, uow)
        {
        }

        protected override OrdenDto ToDto(Orden e) => new()
        {
            Id = e.Id,
            UsuarioId = e.UsuarioId,
            Fecha = e.Fecha,
            Total = e.Total,
            Estado = e.Estado,
            Detalles = e.Detalles.Select(d => new OrdenDetalleDto
            {
                PlatoId = d.PlatoId,
                Cantidad = d.Cantidad,
                PrecioUnitario = d.PrecioUnitario,
                Subtotal = d.Subtotal
            }).ToList()
        };

        protected override void ApplyDto(OrdenDto dto, Orden e)
        {
            if (dto.UsuarioId == Guid.Empty) throw new ArgumentException("UsuarioId requerido");
            if (dto.Detalles is null || dto.Detalles.Count == 0) throw new ArgumentException("Debe incluir detalles");

            e.UsuarioId = dto.UsuarioId;
            e.Fecha = dto.Fecha == default ? DateTime.UtcNow : dto.Fecha;
            e.Estado = dto.Estado;

            e.Detalles.Clear();
            foreach (var item in dto.Detalles)
            {
                if (item.PlatoId == Guid.Empty) throw new ArgumentException("PlatoId inválido");
                if (item.Cantidad <= 0) throw new ArgumentException("Cantidad inválida");
                if (item.PrecioUnitario < 0) throw new ArgumentException("Precio inválido");

                e.Detalles.Add(new OrdenDetalle
                {
                    Id = Guid.NewGuid(),
                    OrdenId = e.Id,
                    PlatoId = item.PlatoId,
                    Cantidad = item.Cantidad,
                    PrecioUnitario = item.PrecioUnitario,
                    Subtotal = item.Cantidad * item.PrecioUnitario
                });
            }

            e.Total = e.Detalles.Sum(x => x.Subtotal);
        }

        public Task<bool> DeleteAsync(RemoveOrdenDto dto, CancellationToken ct = default)
        {
            return Task.FromResult(true);
        }
    }
}