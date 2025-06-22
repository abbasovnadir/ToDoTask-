using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoApp.BackEnd.Application.Interfaces.Repositories;
using ToDoApp.BackEnd.Domain.Entities.Common;
using ToDoApp.BackEnd.Persistence.Contexts.EntityFramework;

namespace ToDoApp.BackEnd.Persistance.Repositories.Concrete.EntityFramework;
public  class EntityReadRepository<T> : IEntityReadRepository<T> where T : class, IEntity
{
    private readonly ToDoAppContext _context;
    private readonly DbSet<T> _dbSet;

    public EntityReadRepository(ToDoAppContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<bool> AnyAsync(Expression<Func<T, bool>> filter) => await _dbSet.AnyAsync(filter);

    public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null) => filter == null ? await _dbSet.CountAsync() : await _dbSet.CountAsync(filter);

    public async Task<T> FindAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false) => asNoTracking ? await _dbSet.AsNoTracking().FirstOrDefaultAsync(filter) : await _dbSet.FirstOrDefaultAsync(filter);

    public async Task<List<T>> GetAllAsync(bool asNoTracking = false) => asNoTracking ? await _dbSet.AsNoTracking().ToListAsync() : await _dbSet.ToListAsync();

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false) => asNoTracking ? await _dbSet.Where(filter).AsNoTracking().ToListAsync() : await _dbSet.Where(filter).ToListAsync();

    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, bool asNoTracking = false)
    {
        var query = _dbSet.AsQueryable();

        if (filter != null)
        {
            query = query.Where(filter);
        }

        if (orderBy != null)
        {
            query = orderBy(query);
        }

        return asNoTracking ? await query.AsNoTracking().ToListAsync() : await query.ToListAsync();
    }

    public async Task<T> GetFilterAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false) => asNoTracking ? await _dbSet.AsNoTracking().FirstOrDefaultAsync(filter) : await _dbSet.FirstOrDefaultAsync(filter);
  
}
