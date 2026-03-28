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

        public override async Task<PagoDto> CreateAsync(PagoDto dto, CancellationToken ct = default)
        {
            if (dto.OrdenId == Guid.Empty)
                throw new ArgumentException("OrdenId requerido");

            // Validar que la orden exista
            var orden = await Uow.Ordenes.GetByIdAsync(dto.OrdenId, ct);
            if (orden is null)
                throw new InvalidOperationException("Orden no existe");

            // Validar que no tenga pago previo
            var pagos = await Uow.Pagos.GetAllAsync(ct);
            if (pagos.Any(p => p.OrdenId == dto.OrdenId))
                throw new InvalidOperationException("La orden ya tiene un pago registrado");

            // Validar que la orden este en estado valido para pagar
            if (orden.Estado == EstadoOrden.Cancelada)
                throw new InvalidOperationException("No se puede pagar una orden cancelada");

            var entity = new Pago
            {
                Id = Guid.NewGuid(),
                OrdenId = dto.OrdenId,
                MetodoPago = dto.MetodoPago,
                Monto = orden.Total,
                EstadoPago = EstadoPago.Pendiente
            };

            // Simulación de pasarela de pago (Stripe futuro)
            bool pagoExitoso = SimularPasarelaPago(entity.MetodoPago, entity.Monto);

            if (pagoExitoso)
            {
                entity.EstadoPago = EstadoPago.Aprobado;

                // Actualizar estado de la orden a EnPreparacion
                orden.Estado = EstadoOrden.EnPreparacion;
                orden.FechaModificacion = DateTime.UtcNow;
                await Uow.Ordenes.UpdateAsync(orden, ct);
            }
            else
            {
                entity.EstadoPago = EstadoPago.Rechazado;
            }

            await Uow.Pagos.AddAsync(entity, ct);
            await Uow.SaveAsync(ct);

            return ToDto(entity);
        }

        public async Task<PagoDto?> GetByOrdenIdAsync(Guid ordenId, CancellationToken ct = default)
        {
            var pagos = await Uow.Pagos.GetAllAsync(ct);
            var pago = pagos.FirstOrDefault(p => p.OrdenId == ordenId);
            return pago is null ? null : ToDto(pago);
        }

        /// <summary>
        /// Simulación de pasarela de pago.
        /// En el futuro se reemplazará por integración real con Stripe API.
        /// </summary>
        private static bool SimularPasarelaPago(MetodoPago metodo, decimal monto)
        {
            if (monto <= 0) return false;
            return true;
        }
    }
}