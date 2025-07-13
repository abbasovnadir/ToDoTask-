using MediatR;
using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailTemplateCommands;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Handlers.EmailTemplateHandler;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Core.Tests.Application.Handlers.BaseFeatureTests;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;

namespace ToDoApp.BackEnd.Core.Tests.Application.Handlers.ApplicationOperationFeatureTests.EmailTemplateHandler;

public class CreateEmailTemplateCommandHandlerTests : BaseCreateHandlerTests<
    CreateEmailTemplateCommand,
    EmailTemplate,
    EmailTemplateListDto>
{
    protected override EmailTemplate CreateEntity()
        => new() { ID = 1, Type = 1, TypeName = "type name", Template = "template" };

    protected override IRequestHandler<CreateEmailTemplateCommand, IDataResult<EmailTemplateListDto>> CreateHandler()
        => new CreateEmailTemplateCommandHandler(MockUow.Object, MockValidator.Object, MockMapper.Object);

    protected override EmailTemplateListDto CreateResponse()
        => new() { ID = 1, Type = 1, TypeName = "type name", Template = "template" };

    protected override CreateEmailTemplateCommand CreateValidCommand()
        => new(1, "template", "user");
}
