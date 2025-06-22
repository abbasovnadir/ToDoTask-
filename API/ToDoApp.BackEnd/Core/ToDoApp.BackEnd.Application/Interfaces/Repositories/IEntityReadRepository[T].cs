using System.Linq.Expressions;
using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Application.Interfaces.Repositories;
public interface IEntityReadRepository<T> where T: class, IEntity
{
    Task<List<T>> GetAllAsync(bool asNotracking = false);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false);
    Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, bool asNoTracking = false);
    Task<T> GetFilterAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false);
    Task<T> FindAsync(Expression<Func<T, bool>> filter, bool asNoTracking = false);
    Task<int> CountAsync(Expression<Func<T, bool>> filter = null);
    Task<bool> AnyAsync(Expression<Func<T, bool>> filter);
}
