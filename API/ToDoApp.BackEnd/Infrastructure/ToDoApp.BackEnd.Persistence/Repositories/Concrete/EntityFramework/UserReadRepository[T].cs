using ToDoApp.BackEnd.Application.Interfaces.Repositories;
using ToDoApp.BackEnd.Domain.Entities.Common;
using ToDoApp.BackEnd.Persistance.Repositories.Concrete.EntityFramework;
using ToDoApp.BackEnd.Persistence.Contexts.EntityFramework;

namespace ToDoApp.BackEnd.Persistance.Repositories.Concrete.EntityFramework;
public sealed class UserReadRepository<T> : EntityReadRepository<T>, IUserReadRepository<T> where T : UserEntity
{
    public UserReadRepository(ToDoAppContext context) : base(context)
    {
    }
}
