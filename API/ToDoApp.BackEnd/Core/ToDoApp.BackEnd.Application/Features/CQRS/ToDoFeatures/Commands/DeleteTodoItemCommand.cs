using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseCommands;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Commands;
public class DeleteTodoItemCommand : DeleteCommandBase<IResult>, IDeleteCommand
{
    public DeleteTodoItemCommand(int id) : base(id)
    {
    }
}
