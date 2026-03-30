using TeAtiendo.Domain.Entities.Admin;

namespace TeAtiendo.Domain.Interfaces
{
    public interface IModeracionContenidoRepository : IRepository<ModeracionContenido>
    {
        Task<IReadOnlyList<ModeracionContenido>> GetByAdminAsync(Guid adminId, CancellationToken ct = default);
        Task<IReadOnlyList<ModeracionContenido>> GetByTipoContenidoAsync(string tipoContenido, CancellationToken ct = default);
        Task<IReadOnlyList<ModeracionContenido>> GetByEstadoAsync(string estado, CancellationToken ct = default);
    }
}