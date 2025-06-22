using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoApp.BackEnd.Application.Interfaces.Repositories;
using ToDoApp.BackEnd.Domain.Entities.Common;
using ToDoApp.BackEnd.Persistence.Contexts.EntityFramework;

namespace ToDoApp.BackEnd.Persistance.Repositories.Concrete.EntityFramework;
public class EntityWriteRepository<T> : IEntityWriteRepository<T> where T : class, IEntity
{
    private readonly ToDoAppContext _context;
    private readonly DbSet<T> _dbSet;

    public EntityWriteRepository(ToDoAppContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public void DeleteAsync(T entity)
    {
        _dbSet.Remove(entity);
    }
    public T Update(Expression<Func<T, bool>> filter, T entity)
    {
        var trackedEntity = _dbSet.FirstOrDefault(filter);

        if (trackedEntity is not null)
        {
            _context.Entry(trackedEntity).CurrentValues.SetValues(entity);
        }

        return entity;
    }
}
