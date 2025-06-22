using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Application.Interfaces.Repositories;
public interface IUserReadRepository<T> : IEntityReadRepository<T> where T : UserEntity;
