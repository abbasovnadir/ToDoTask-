using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailParameterCommands;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Handlers.EmailParameterHandlers;
internal sealed class DeleteEmailParameterCommandHandler : BaseDeleteCommandHandler<DeleteEmailParameterCommand, EmailParameter>
{
    public DeleteEmailParameterCommandHandler(IUnitOfWork uow) : base(uow)
    {
    }
}
