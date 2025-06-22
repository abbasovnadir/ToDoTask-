using AutoMapper;
using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Commands.EmailParameterCommands;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;
using FluentValidation;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Handlers.EmailParameterHandlers
{
    internal sealed class UpdateEmailParameterCommandHandler : BaseUpdateCommandHandler<UpdateEmailParameterCommand, EmailParameter, EmailParameterListDto>
    {
        public UpdateEmailParameterCommandHandler(IUnitOfWork uow, IMapper mapper, IValidator<UpdateEmailParameterCommand> validator) : base(uow, mapper, validator)
        {
        }
    }
}
