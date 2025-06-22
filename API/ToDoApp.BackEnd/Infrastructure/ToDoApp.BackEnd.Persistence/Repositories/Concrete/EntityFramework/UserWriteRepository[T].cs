using ToDoApp.BackEnd.Application.Interfaces.Repositories;
using ToDoApp.BackEnd.Domain.Entities.Common;
using ToDoApp.BackEnd.Persistence.Contexts.EntityFramework;

namespace ToDoApp.BackEnd.Persistance.Repositories.Concrete.EntityFramework;
public sealed class UserWriteRepository<T> : EntityWriteRepository<T>, IUserWriteRepository<T> where T : UserEntity
{
    public UserWriteRepository(ToDoAppContext context) : base(context)
    {
    }
   
}
