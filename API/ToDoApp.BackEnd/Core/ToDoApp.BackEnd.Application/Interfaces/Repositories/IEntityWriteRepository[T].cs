using System.Linq.Expressions;
using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Application.Interfaces.Repositories
{
    public interface IEntityWriteRepository<T> where T : class,IEntity
    {
        Task<T> AddAsync(T entity);
        void DeleteAsync(T entity);
        T Update(Expression<Func<T, bool>> filter, T entity);
    }
}
