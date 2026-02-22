using System;
using System.Collections.Generic;
using System.Linq;
using TeAtiendo.Application.DTOs;

namespace TeAtiendo.Application.Services
{
    public class AuthService
    {

        private static readonly List<UsuarioDto> _usuarios = new List<UsuarioDto>();
        private static int _id = 1;

        public AuthResult Register(RegisterRequest request)
        {
            // TODO: validar según reglas del SAD/SRS
            var u = new UsuarioDto
            {
                IdUsuario = _id++,
                Nombre = request.Nombre,
                Correo = request.Correo,
                Rol = request.Rol,
                FechaRegistro = DateTime.UtcNow,
                Estado = "Activo"
            };
            _usuarios.Add(u);

            return new AuthResult
            {
                IdUsuario = u.IdUsuario,
                Rol = u.Rol,
                TokenSimulado = "token-simulado-" + u.IdUsuario
            };
        }

        public AuthResult? Login(LoginRequest request)
        {
            // TODO: autenticar real con hash en BD (luego)
            var u = _usuarios.FirstOrDefault(x => x.Correo.Equals(request.Correo, StringComparison.OrdinalIgnoreCase));
            if (u is null) return null;

            return new AuthResult
            {
                IdUsuario = u.IdUsuario,
                Rol = u.Rol,
                TokenSimulado = "token-simulado-" + u.IdUsuario
            };
        }
    }
}
