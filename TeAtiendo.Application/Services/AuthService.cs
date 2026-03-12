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

        public AuthService(TeAtiendo.Persistence.Interface.IUnitOfWork uow)
        {
            _uow = uow;
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

            return new LoginResultDto
            {
                Success = true,
                Message = "OK",
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
    }
}