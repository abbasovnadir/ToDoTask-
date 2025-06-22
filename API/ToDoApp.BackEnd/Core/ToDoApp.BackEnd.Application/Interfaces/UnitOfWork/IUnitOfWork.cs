using ToDoApp.BackEnd.Application.Interfaces.Repositories;
using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
public interface IUnitOfWork : IDisposable
{
    IReadRepository<T> ReadRepository<T>() where T : BaseEntity;
    IWriteRepository<T> WriteRepository<T>() where T : BaseEntity;
    IUserReadRepository<T> UserReadRepository<T>() where T : UserEntity;
    IUserWriteRepository<T> UserWriteRepository<T>() where T : UserEntity;
    void BeginTransaction();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

    Task<int> SaveChangesAsync();
}
