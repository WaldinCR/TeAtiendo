using Microsoft.EntityFrameworkCore;
using TeAtiendo.Domain.Entities.Auditory;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Auditoria
{
    internal class AuditoriaLogRepository
    {
        private readonly TeAtiendoContext _context;

        public AuditoriaLogRepository(TeAtiendoContext context)
        {
            _context = context;
        }

        public Task RegistrarAsync(string accion, string? usuarioCorreo = null, string? detalles = null)
        {
            // Base: sin DbSet<AuditoriaLog>, no se guarda en BD.
            // De momento solo “simula” el registro.
            _ = accion;
            _ = usuarioCorreo;
            _ = detalles;

            return Task.CompletedTask;
        }
    public class AuditoriaRepository
    {
        private readonly TeAtiendoContext _context;

        public AuditoriaRepository(TeAtiendoContext context)
        {
            _context = context;
        }

        public async Task<List<Auditoria>> GetAllAsync()
        {
            // Si Auditoria tiene propiedad Fecha, ordenamos por ella.
            // Si no la tiene, quita el OrderByDescending.
            return await _context.Set<Auditoria>()
                .OrderByDescending(a => a.Fecha)
                .ToListAsync();
        }

        public async Task<Auditoria?> GetByIdAsync(Guid id)
        {
            // Guid Id viene de BaseEntity
            return await _context.Set<Auditoria>()
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task AddAsync(Auditoria auditoria)
        {
            await _context.Set<Auditoria>().AddAsync(auditoria);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var auditoria = await GetByIdAsync(id);
            if (auditoria is null) return;

            _context.Set<Auditoria>().Remove(auditoria);
            await _context.SaveChangesAsync();
        }
    }
}