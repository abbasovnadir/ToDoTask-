using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Commands;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Domain.Entities;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Handlers;
internal sealed class DeleteTodoItemCommandHandler : BaseDeleteCommandHandler<DeleteTodoItemCommand, TodoItem>
{
    public DeleteTodoItemCommandHandler(IUnitOfWork uow) : base(uow)
    {
    }
}
