using MediatR;
using ToDoApp.BackEnd.Application.Dtos.TodoItemDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Commands;
using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Handlers;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Core.Tests.Application.Handlers.BaseFeatureTests;
using ToDoApp.BackEnd.Domain.Entities;
using ToDoApp.BackEnd.Domain.Enums;

namespace ToDoApp.BackEnd.Core.Tests.Application.Handlers.ToDoFeaturesTests
{
    public class CreateTodoItemCommandHandlerTests : BaseCreateHandlerTests<
        CreateTodoItemCommand,
        TodoItem,
        TodoItemListDto>
    {

        protected override CreateTodoItemCommand CreateValidCommand()
           => new("title", "desc", DateTime.Now, TodoStatus.Pending, Guid.NewGuid(), "extra", new CommandInfo());

        protected override TodoItem CreateEntity()
            => new() { ID = 1, Title = "title", Description = "desc", DueDate = DateTime.Now.AddDays(1), Status = TodoStatus.Pending, UserId = Guid.NewGuid() };

        protected override TodoItemListDto CreateResponse()
            => new() { ID = 1, Title = "title", Description = "desc", DueDate = DateTime.Now.AddDays(1), Status = TodoStatus.Pending, UserId = Guid.NewGuid() };

        protected override IRequestHandler<CreateTodoItemCommand, IDataResult<TodoItemListDto>> CreateHandler()
            => new CreateTodoItemCommandHandler(MockUow.Object, MockValidator.Object, MockMapper.Object);
    }   
}
