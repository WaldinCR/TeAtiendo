using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TeAtiendo.Application.DTOs.Auth;
using TeAtiendo.Application.DTOs.Usuario;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Application.Security;
using TeAtiendo.Domain.Entities.Segurity;

namespace TeAtiendo.Application.Services
{
    public sealed class AuthService : IAuthService
    {
        private readonly TeAtiendo.Persistence.Interface.IUnitOfWork _uow;
        private readonly IConfiguration _configuration;

        public AuthService(TeAtiendo.Persistence.Interface.IUnitOfWork uow, IConfiguration configuration)
        {
            _uow = uow;
            _configuration = configuration;
        }

        public async Task<UsuarioDto> RegisterAsync(RegisterDto dto, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre)) throw new ArgumentException("Nombre requerido");
            if (string.IsNullOrWhiteSpace(dto.Correo)) throw new ArgumentException("Correo requerido");
            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 8)
                throw new ArgumentException("Password inválido (mínimo 8 caracteres)");

            var email = dto.Correo.Trim().ToLowerInvariant();

            var existing = (await _uow.Usuarios.GetAllAsync(ct))
                .FirstOrDefault(u => u.Correo.ToLower() == email);

            if (existing is not null)
                throw new InvalidOperationException("Ya existe un usuario con ese correo.");

            var user = new Usuario
            {
                Id = Guid.NewGuid(),
                Nombre = dto.Nombre.Trim(),
                Correo = email,
                Rol = dto.Rol,
                PasswordHash = PasswordHasher.Hash(dto.Password)
            };

            await _uow.Usuarios.AddAsync(user, ct);
            await _uow.SaveAsync(ct);

            return new UsuarioDto
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Correo = user.Correo,
                Rol = user.Rol
            };
        }

        public async Task<LoginResultDto> LoginAsync(LoginDto dto, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(dto.Correo) || string.IsNullOrWhiteSpace(dto.Password))
                return new LoginResultDto { Success = false, Message = "Correo y password requeridos." };

            var email = dto.Correo.Trim().ToLowerInvariant();

            var user = (await _uow.Usuarios.GetAllAsync(ct))
                .FirstOrDefault(u => u.Correo.ToLower() == email);

            if (user is null)
                return new LoginResultDto { Success = false, Message = "Credenciales inválidas." };

            if (!PasswordHasher.Verify(dto.Password, user.PasswordHash))
                return new LoginResultDto { Success = false, Message = "Credenciales inválidas." };

            var token = GenerarToken(user);

            return new LoginResultDto
            {
                Success = true,
                Message = "OK",
                Token = token,
                User = new UsuarioDto
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Correo = user.Correo,
                    Rol = user.Rol
                }
            };
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword, CancellationToken ct = default)
        {
            if (userId == Guid.Empty) return false;

            var user = await _uow.Usuarios.GetByIdAsync(userId, ct);
            if (user is null) return false;

            if (!PasswordHasher.Verify(currentPassword, user.PasswordHash))
                throw new InvalidOperationException("Password actual incorrecta.");

            user.PasswordHash = PasswordHasher.Hash(newPassword);
            await _uow.Usuarios.UpdateAsync(user, ct);
            await _uow.SaveAsync(ct);

            return true;
        }

        // token JWT 
        private string GenerarToken(Usuario user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Secret"]!));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Correo),
                new Claim(ClaimTypes.Name, user.Nombre),
                new Claim(ClaimTypes.Role, user.Rol.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var expiracion = int.Parse(jwtSettings["ExpirationMinutes"] ?? "60");

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(expiracion),
                signingCredentials: new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}