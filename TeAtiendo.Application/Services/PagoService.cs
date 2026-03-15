using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Pago;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Operations;
using TeAtiendo.Domain.Enums;

namespace TeAtiendo.Application.Services
{
    public sealed class PagoService : BaseService<Pago, PagoDto>, IPagoService
    {
        public PagoService(TeAtiendo.Persistence.Interface.IUnitOfWork uow)
            : base(uow.Pagos, uow)
        {
        }

        protected override PagoDto ToDto(Pago e) => new()
        {
            Id = e.Id,
            OrdenId = e.OrdenId,
            Monto = e.Monto,
            EstadoPago = e.EstadoPago,
            MetodoPago = e.MetodoPago
        };

        protected override void ApplyDto(PagoDto dto, Pago e)
        {
            if (dto.OrdenId == Guid.Empty)
                throw new ArgumentException("OrdenId requerido");

            e.OrdenId = dto.OrdenId;
            e.MetodoPago = dto.MetodoPago;

         
            e.Monto = dto.Monto;
            e.EstadoPago = dto.EstadoPago;
        }

        //   PagoDto
        public override async Task<PagoDto> CreateAsync(PagoDto dto, CancellationToken ct = default)
        {
            if (dto.OrdenId == Guid.Empty) throw new ArgumentException("OrdenId requerido");

            var orden = await Uow.Ordenes.GetByIdAsync(dto.OrdenId, ct);
            if (orden is null) throw new InvalidOperationException("Orden no existe");

            var pagos = await Uow.Pagos.GetAllAsync(ct);
            if (pagos.Any(p => p.OrdenId == dto.OrdenId))
                throw new InvalidOperationException("La orden ya tiene un pago registrado");

            var entity = new Pago
            {
                Id = Guid.NewGuid(),
                OrdenId = dto.OrdenId,
                MetodoPago = dto.MetodoPago,
                Monto = orden.Total, //total de la orden
                EstadoPago = EstadoPago.Pendiente
            };

            await Uow.Pagos.AddAsync(entity, ct);
            await Uow.SaveAsync(ct);

            return ToDto(entity);
        }
    }
}