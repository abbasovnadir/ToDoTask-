using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseCommands;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Commands.EmailTemplateCommands;
public sealed class DeleteEmailTemplateCommand : DeleteCommandBase<IResult>, IDeleteCommand
{
    public DeleteEmailTemplateCommand(int id) : base(id)
    {
    }
}
