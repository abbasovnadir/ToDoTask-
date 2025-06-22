using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Application.Interfaces.Repositories;
public interface IReadRepository<T> : IEntityReadRepository<T> where T : BaseEntity;
