using System;
using System.Collections.Generic;
using System.Linq;
using TeAtiendo.Application.DTOs;

namespace TeAtiendo.Application.Services
{
    public class OrdenService
    {
        private static readonly List<OrdenDto> _ordenes = new List<OrdenDto>();
        private static int _id = 1;

        public OrdenDto Crear(OrdenCreateRequest request)
        {
            var orden = new OrdenDto
            {
                IdOrden = _id++,
                IdUsuario = request.IdUsuario,
                IdRestaurante = request.IdRestaurante,
                FechaCreacion = DateTime.UtcNow,
                Estado = "Pendiente",
                Subtotal = 0m,
                Total = 0m,
                Detalles = new List<OrdenDetalleDto>()
            };

            // Base: crea detalles, sin catálogo real aún (universitario)
            foreach (var d in request.Detalles)
            {
                var detalle = new OrdenDetalleDto
                {
                    IdPlato = d.IdPlato,
                    NombrePlato = "Plato #" + d.IdPlato, // TODO: reemplazar por nombre real del catálogo
                    Cantidad = d.Cantidad,
                    PrecioUnitario = 0m, // TODO: cargar precio real
                    TotalLinea = 0m
                };

                orden.Detalles.Add(detalle);
            }

            // Totales (a futuro: precios reales)
            orden.Subtotal = orden.Detalles.Sum(x => x.TotalLinea);
            orden.Total = orden.Subtotal;

            _ordenes.Add(orden);
            return orden;
        }

        public OrdenDto? ObtenerPorId(int idOrden)
        {
            return _ordenes.FirstOrDefault(x => x.IdOrden == idOrden);
        }

        public List<OrdenDto> ObtenerPorUsuario(int idUsuario)
        {
            return _ordenes.Where(x => x.IdUsuario == idUsuario).ToList();
        }

        public void CambiarEstado(int idOrden, string nuevoEstado)
        {
            var orden = _ordenes.FirstOrDefault(x => x.IdOrden == idOrden);
            if (orden != null)
            {
                orden.Estado = nuevoEstado;
            }
        }
    }
}
