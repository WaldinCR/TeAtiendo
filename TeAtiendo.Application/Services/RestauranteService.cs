using System;
using System.Collections.Generic;
using System.Linq;
using TeAtiendo.Application.DTOs;

namespace TeAtiendo.Application.Services
{
    public class RestauranteService
    {
        private static readonly List<RestauranteDto> _restaurantes = new List<RestauranteDto>();
        private static int _id = 1;

        public RestauranteDto Crear(RestauranteCreateRequest request)
        {
            var r = new RestauranteDto
            {
                IdRestaurante = _id++,
                Nombre = request.Nombre,
                Direccion = request.Direccion,
                Telefono = request.Telefono,
                HoraApertura = request.HoraApertura,
                HoraCierre = request.HoraCierre,
                Estado = "Activo"
            };

            _restaurantes.Add(r);
            return r;
        }

        public List<RestauranteDto> ObtenerTodos()
        {
            return _restaurantes;
        }

        public RestauranteDto? ObtenerPorId(int id)
        {
            return _restaurantes.FirstOrDefault(x => x.IdRestaurante == id);
        }
    }
}
