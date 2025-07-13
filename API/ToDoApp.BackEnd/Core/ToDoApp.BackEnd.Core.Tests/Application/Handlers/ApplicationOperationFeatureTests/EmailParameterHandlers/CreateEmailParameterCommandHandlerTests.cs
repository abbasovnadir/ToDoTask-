using MediatR;
using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailParameterCommands;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Handlers.EmailParameterHandlers;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Core.Tests.Application.Handlers.BaseFeatureTests;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;

namespace ToDoApp.BackEnd.Core.Tests.Application.Handlers.ApplicationOperationFeatureTests.EmailParameterHandlers;
public class CreateEmailParameterCommandHandlerTests : BaseCreateHandlerTests<
    CreateEmailParameterCommand,
    EmailParameter,
    EmailParameterListDto>
{
    protected override CreateEmailParameterCommand CreateValidCommand()
        => new("noreply@gmail.com", "pass", "smtp.gmail.com",  587, true, (int)CommandInfo.RowStatusIsActive,"user");

    protected override EmailParameter CreateEntity()
        => new() { ID = 1, SMTP = "smtp.gmail.com", Email = "noreply@gmail.com", Password = "pass", Port = 587, SSL = true };

    protected override EmailParameterListDto CreateResponse()
        => new() { ID = 1, SMTP = "smtp.gmail.com", Email = "noreply@gmail.com", Port = 587, SSL = true };

    protected override IRequestHandler<CreateEmailParameterCommand, IDataResult<EmailParameterListDto>> CreateHandler()
        => new CreateEmailParameterCommandHandler(MockUow.Object, MockValidator.Object, MockMapper.Object);
}

