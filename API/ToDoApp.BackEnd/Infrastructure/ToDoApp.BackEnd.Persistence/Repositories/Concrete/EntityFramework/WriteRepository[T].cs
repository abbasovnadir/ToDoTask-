using ToDoApp.BackEnd.Application.Interfaces.Repositories;
using ToDoApp.BackEnd.Domain.Entities.Common;
using ToDoApp.BackEnd.Persistance.Repositories.Concrete.EntityFramework;
using ToDoApp.BackEnd.Persistence.Contexts.EntityFramework;

namespace ToDoApp.BackEnd.Persistance.Repositories.Concrete.EntityFramework;
public sealed class WriteRepository<T> : EntityWriteRepository<T>, IWriteRepository<T> where T : BaseEntity
{
        public WriteRepository(ToDoAppContext context) : base(context)
    {
    }   
}
