using Microsoft.EntityFrameworkCore;
using TeAtiendo.Domain.Entities.Segurity;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Repositories.Auditoria
{
    public class AuditoriaLogRepository
    {
        private readonly TeAtiendoContext _context;

        public AuditoriaLogRepository(TeAtiendoContext context)
        {
            _context = context;
        }

        public async Task<List<AuditoriaLog>> GetAllAsync()
        {
            return await _context.Set<AuditoriaLog>()
                                 .OrderByDescending(a => a.Fecha)
                                 .ToListAsync();
        }

        public async Task<AuditoriaLog?> GetByIdAsync(int id)
        {
            return await _context.Set<AuditoriaLog>()
                                 .FirstOrDefaultAsync(a => a.IdLog == id);
        }

        public async Task AddAsync(AuditoriaLog log)
        {
            await _context.Set<AuditoriaLog>().AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var log = await GetByIdAsync(id);
            if (log != null)
            {
                _context.Remove(log);
                await _context.SaveChangesAsync();
            }
        }
    }
}