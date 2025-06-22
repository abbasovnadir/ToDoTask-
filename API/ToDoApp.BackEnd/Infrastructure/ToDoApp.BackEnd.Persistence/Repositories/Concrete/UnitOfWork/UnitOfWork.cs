using ToDoApp.BackEnd.Persistance.Repositories.Concrete.EntityFramework;
using Microsoft.EntityFrameworkCore.Storage;
using ToDoApp.BackEnd.Application.Interfaces.Repositories;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Domain.Entities.Common;
using ToDoApp.BackEnd.Persistence.Contexts.EntityFramework;

namespace ToDoApp.BackEnd.Persistance.Repositories.Concrete.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly ToDoAppContext _context;
    private IDbContextTransaction _currentTransaction;

    public UnitOfWork(ToDoAppContext context) => _context = context;

    public void Dispose() => _context.Dispose();

    #region Entity Repository
    public IReadRepository<T> ReadRepository<T>() where T : BaseEntity => new ReadRepository<T>(_context);

    public IWriteRepository<T> WriteRepository<T>() where T : BaseEntity => new WriteRepository<T>(_context);
    #endregion

    #region User Repository
    public IUserReadRepository<T> UserReadRepository<T>() where T : UserEntity => new UserReadRepository<T>(_context);
    
    public IUserWriteRepository<T> UserWriteRepository<T>() where T : UserEntity => new UserWriteRepository<T>(_context);
    #endregion

    // Transaction start
    public void BeginTransaction()
    {
        if (_currentTransaction == null)
        {
            _currentTransaction = _context.Database.BeginTransaction();
        }
    }

    // Transaction commit
    public async Task CommitTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.CommitAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    // Transaction rollback 
    public async Task RollbackTransactionAsync()
    {
        if (_currentTransaction != null)
        {
            await _currentTransaction.RollbackAsync();
            await _currentTransaction.DisposeAsync();
            _currentTransaction = null;
        }
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
    
}
