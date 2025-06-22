using AutoMapper;
using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Commands.EmailTemplateCommands;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;
using FluentValidation;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Handlers.EmailTemplateHandler;

internal sealed class UpdateEmailTemplateCommandHandler : BaseUpdateCommandHandler<UpdateEmailTemplateCommand, EmailTemplate, EmailTemplateListDto>
{
    public UpdateEmailTemplateCommandHandler(IUnitOfWork uow, IMapper mapper, IValidator<UpdateEmailTemplateCommand> validator) : base(uow, mapper, validator)
    {
    }
}
