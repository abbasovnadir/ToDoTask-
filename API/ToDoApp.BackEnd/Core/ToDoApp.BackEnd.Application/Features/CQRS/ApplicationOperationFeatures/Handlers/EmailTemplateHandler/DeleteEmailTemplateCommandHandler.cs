using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailTemplateCommands;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Handlers.EmailTemplateHandler;
internal sealed class DeleteEmailTemplateCommandHandler : BaseDeleteCommandHandler<DeleteEmailTemplateCommand, EmailTemplate>
{
    public DeleteEmailTemplateCommandHandler(IUnitOfWork uow) : base(uow)
    {
    }
}
