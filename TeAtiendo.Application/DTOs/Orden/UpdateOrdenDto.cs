using System;
using TeAtiendo.Domain.Entities.Operations;

namespace TeAtiendo.Application.DTOs.Orden
{
    public class UpdateOrdenDto
    {
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }
        public Guid RestauranteId { get; set; }
        public EstadoOrden EstadoOrden { get; set; }
        public decimal Total { get; set; }
    }
}