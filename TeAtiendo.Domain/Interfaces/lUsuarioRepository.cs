using TeAtiendo.Domain.Entities.Segurity;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task<Usuario?> GetByCorreoAsync(string correo, CancellationToken ct = default);
    }
}