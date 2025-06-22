using ToDoApp.BackEnd.Application.Interfaces.Repositories;
using ToDoApp.BackEnd.Domain.Entities.Common;
using ToDoApp.BackEnd.Persistence.Contexts.EntityFramework;

namespace ToDoApp.BackEnd.Persistance.Repositories.Concrete.EntityFramework;
public sealed class ReadRepository<T> : EntityReadRepository<T>, IReadRepository<T> where T : BaseEntity
{
    public ReadRepository(ToDoAppContext context) : base(context)
    {
    }
}
