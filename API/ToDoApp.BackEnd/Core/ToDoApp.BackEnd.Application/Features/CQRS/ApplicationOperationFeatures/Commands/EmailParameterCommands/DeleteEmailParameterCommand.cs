using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseCommands;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailParameterCommands;
public sealed class DeleteEmailParameterCommand : DeleteCommandBase<IResult>, IDeleteCommand
{
    public DeleteEmailParameterCommand(int id) : base(id) => ID = id;

}
