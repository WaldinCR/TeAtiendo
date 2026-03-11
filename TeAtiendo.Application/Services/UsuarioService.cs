using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeAtiendo.Application.DTOs.Usuario;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Domain.Interfaces;

namespace TeAtiendo.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<IEnumerable<UsuarioDto>> GetAllAsync()
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            return usuarios.Select(u => new UsuarioDto
            {
                IdUsuario = u.IdUsuario,
                Nombre = u.Nombre,
                Correo = u.Correo,
                Rol = u.Rol,
                Estado = u.Estado,
                FechaRegistro = u.FechaRegistro
            });
        }

        public async Task<UsuarioDto?> GetByIdAsync(int id)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null) return null;

            return new UsuarioDto
            {
                IdUsuario = usuario.IdUsuario,
                Nombre = usuario.Nombre,
                Correo = usuario.Correo,
                Rol = usuario.Rol,
                Estado = usuario.Estado,
                FechaRegistro = usuario.FechaRegistro
            };
        }

        public async Task<UsuarioDto> AddAsync(UsuarioDto dto)
        {
            var usuario = new Usuario
            {
                Nombre = dto.Nombre,
                Correo = dto.Correo,
                Rol = dto.Rol,
                Estado = dto.Estado,
                FechaRegistro = dto.FechaRegistro,
                PasswordHash = string.Empty
            };

            await _usuarioRepository.AddAsync(usuario);

            dto.IdUsuario = usuario.IdUsuario;
            return dto;
        }

        public async Task UpdateAsync(int id, UsuarioDto dto)
        {
            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
                throw new Exception("Usuario no encontrado.");

            usuario.Nombre = dto.Nombre;
            usuario.Correo = dto.Correo;
            usuario.Rol = dto.Rol;
            usuario.Estado = dto.Estado;
            usuario.FechaRegistro = dto.FechaRegistro;

            await _usuarioRepository.UpdateAsync(usuario);
        }

        public async Task DeleteAsync(int id)
        {
            await _usuarioRepository.DeleteAsync(id);
        }
    }
}