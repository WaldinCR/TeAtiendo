using System;
using System.Collections.Generic;
using System.Linq;
using TeAtiendo.Application.DTOs;

namespace TeAtiendo.Application.Services
{
    public class ReservaService
    {
        private static readonly List<ReservaDto> _reservas = new List<ReservaDto>();
        private static int _id = 1;

        public ReservaDto Crear(ReservaCreateRequest request)
        {
            var r = new ReservaDto
            {
                IdReserva = _id++,
                IdUsuario = request.IdUsuario,
                IdRestaurante = request.IdRestaurante,
                IdMesa = request.IdMesa,
                Fecha = request.Fecha,
                Hora = request.Hora,
                CantidadPersonas = request.CantidadPersonas,
                Estado = "Pendiente"
            };

            _reservas.Add(r);
            return r;
        }

        public List<ReservaDto> ObtenerPorUsuario(int idUsuario)
        {
            return _reservas.Where(x => x.IdUsuario == idUsuario).ToList();
        }

        public void CambiarEstado(int idReserva, string nuevoEstado)
        {
            var reserva = _reservas.FirstOrDefault(x => x.IdReserva == idReserva);
            if (reserva != null)
            {
                reserva.Estado = nuevoEstado;
            }
        }
    }
}
