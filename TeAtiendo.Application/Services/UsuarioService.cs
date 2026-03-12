using TeAtiendo.Application.Base;
using TeAtiendo.Application.DTOs.Usuario;
using TeAtiendo.Application.Interfaces;
using TeAtiendo.Domain.Entities.Segurity;

namespace TeAtiendo.Application.Services
{
    public sealed class UsuarioService : BaseService<Usuario, UsuarioDto>, IUsuarioService
    {
        public UsuarioService(TeAtiendo.Persistence.Interface.IUnitOfWork uow)
            : base(uow.Usuarios, uow)
        {
        }

        protected override UsuarioDto ToDto(Usuario entity) => new()
        {
            Id = entity.Id,
            Nombre = entity.Nombre,
            Correo = entity.Correo,
            Rol = entity.Rol
        };

        protected override void ApplyDto(UsuarioDto dto, Usuario entity)
        {
            if (string.IsNullOrWhiteSpace(dto.Nombre)) throw new ArgumentException("Nombre requerido");
            if (string.IsNullOrWhiteSpace(dto.Correo)) throw new ArgumentException("Correo requerido");

            entity.Nombre = dto.Nombre.Trim();
            entity.Correo = dto.Correo.Trim().ToLowerInvariant();
            entity.Rol = dto.Rol;
        }

        public async Task<UsuarioDto?> GetByEmailAsync(string correo, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(correo)) return null;
            var normalized = correo.Trim().ToLowerInvariant();

            var users = await Uow.Usuarios.GetAllAsync(ct);
            var user = users.FirstOrDefault(u => u.Correo.ToLower() == normalized);
            return user is null ? null : ToDto(user);
        }
    }
}