using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeAtiendo.Persistence.Context;

namespace TeAtiendo.Persistence.Base
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

    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _context.Entry(entity).Property(x => x.FechaCreacion).IsModified = false;
    }

    public void Delete(T entity)
    {
        entity.Activo = false;
        _dbSet.Update(entity);
    }