using System;
using System.Collections.Generic;
using System.Linq;
using TeAtiendo.Application.DTOs;

namespace TeAtiendo.Application.Services
{
    public class UsuarioService
    {
        private static readonly List<UsuarioDto> _usuarios = new List<UsuarioDto>();
        private static int _id = 1;

        public UsuarioDto Crear(UsuarioDto dto)
        {
            dto.IdUsuario = _id++;
            dto.FechaRegistro = DateTime.UtcNow;
            _usuarios.Add(dto);
            return dto;
        }

        public List<UsuarioDto> ObtenerTodos()
        {
            return _usuarios;
        }

        public UsuarioDto? ObtenerPorId(int id)
        {
            return _usuarios.FirstOrDefault(x => x.IdUsuario == id);
        }
    }
}
