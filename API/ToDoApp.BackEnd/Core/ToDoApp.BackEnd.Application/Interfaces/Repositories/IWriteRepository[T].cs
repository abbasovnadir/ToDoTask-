using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Application.Interfaces.Repositories;
public interface IWriteRepository<T> : IEntityWriteRepository<T> where T : BaseEntity;
