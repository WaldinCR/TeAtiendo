using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeAtiendo.Domain.Base;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Base
{
    public class BaseRepository<T> where T : BaseEntity
    {
        protected readonly TeAtiendoContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(TeAtiendoContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.Where(x => x.Activo).ToListAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.Activo);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.Activo = false;
                _dbSet.Update(entity);
            }
            await Task.CompletedTask;
        }
    }
}