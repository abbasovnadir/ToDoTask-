using AutoMapper;
using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailTemplateCommands;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;
using FluentValidation;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Handlers.EmailTemplateHandler;
public sealed class CreateEmailTemplateCommandHandler : BaseCreateCommandHandler<CreateEmailTemplateCommand, EmailTemplate, EmailTemplateListDto>
{
    public CreateEmailTemplateCommandHandler(IUnitOfWork unitOfWork, IValidator<CreateEmailTemplateCommand> validator, IMapper mapper) : base(unitOfWork, validator, mapper)
    {
    }
}
