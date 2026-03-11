using System;
using System.Linq;
using System.Threading.Tasks;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Interfaces;

namespace TeAtiendo.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AuthService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> LoginAsync(string correo, string password)
        {
            var usuarios = await _usuarioRepository.GetAllAsync();

            var usuario = usuarios.FirstOrDefault(u => u.Correo == correo);

            if (usuario == null)
                return false;

            return usuario.PasswordHash == password;
        }
    }
}