using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Application.Interfaces.Repositories;
public interface IUserWriteRepository<T> : IEntityWriteRepository<T> where T : UserEntity;
