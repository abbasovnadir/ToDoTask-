using AutoMapper;
using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailParameterCommands;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;
using FluentValidation;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Handlers.EmailParameterHandlers;
public sealed class CreateEmailParameterCommandHandler : BaseCreateCommandHandler<CreateEmailParameterCommand, EmailParameter, EmailParameterListDto>
{
    public CreateEmailParameterCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateEmailParameterCommand> validator, IMapper mapper) : base(unitOfWork, validator, mapper)
    {
    }
}
