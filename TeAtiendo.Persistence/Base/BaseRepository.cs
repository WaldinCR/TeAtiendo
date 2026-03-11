using Microsoft.EntityFrameworkCore;
using TeAtiendo.Domain.Base;
using TeAtiendo.Domain.Interfaces;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Base
{

    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly TeAtiendoContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(TeAtiendoContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(CancellationToken ct = default)
        {
            return await _dbSet
                .Where(x => x.Activo)
                .ToListAsync(ct);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.Activo, ct);
        }

        public async Task AddAsync(T entity, CancellationToken ct = default)
        {
            await _dbSet.AddAsync(entity, ct);
        }

        public Task UpdateAsync(T entity, CancellationToken ct = default)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public async Task SoftDeleteAsync(Guid id, Guid userId, CancellationToken ct = default)
        {
            var entity = await GetByIdAsync(id, ct);
            if (entity is null) return;

            entity.Activo = false;
            entity.UserDeleted = userId;
            entity.DeletedDate = DateTime.UtcNow;

            _dbSet.Update(entity);
        }
    }
}