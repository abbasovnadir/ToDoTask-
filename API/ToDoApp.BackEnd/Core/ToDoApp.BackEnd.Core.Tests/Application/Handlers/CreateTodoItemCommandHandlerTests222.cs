//using AutoMapper;
//using FluentValidation;
//using Moq;
//using ToDoApp.BackEnd.Application.Dtos.TodoItemDtos;
//using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Commands;
//using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Handlers;
//using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
//using ToDoApp.BackEnd.Application.Utilities.Enums;
//using ToDoApp.BackEnd.Domain.Entities;
//using ToDoApp.BackEnd.Domain.Enums;


//namespace ToDoApp.BackEnd.Core.Tests.Application.Handlers;
//public class CreateTodoItemCommandHandlerTests
//{
//    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
//    private readonly Mock<IValidator<CreateTodoItemCommand>> _mockValidator;
//    private readonly Mock<IMapper> _mockMapper;

//    private readonly CreateTodoItemCommandHandler _handler;

//    public CreateTodoItemCommandHandlerTests()
//    {
//        _mockUnitOfWork = new Mock<IUnitOfWork>();
//        _mockValidator = new Mock<IValidator<CreateTodoItemCommand>>();
//        _mockMapper = new Mock<IMapper>();

//        _handler = new CreateTodoItemCommandHandler(
//            _mockUnitOfWork.Object,
//            _mockValidator.Object,
//            _mockMapper.Object);
//    }

//    [Fact]
//    public async Task Handle_ValidRequest_ReturnsSuccessDataResult()
//    {
//        // Arrange
//        var userId = Guid.NewGuid();
//        var dueDate = new DateTime(2025, 7, 10);

//        var command = new CreateTodoItemCommand
//            (
//             "Title",
//             "Description",
//             dueDate,
//             TodoStatus.Pending,
//             userId,
//             "Some string",
//             new CommandInfo()
//            );

//        _mockValidator.Setup(v => v.Validate(command))
//            .Returns(new FluentValidation.Results.ValidationResult());

//        var todoEntity = new TodoItem
//        {
//            ID = 1,
//            Title = "Test Task Title",
//            Description = "This is a description for test task.",
//            DueDate = dueDate,
//            Status = TodoStatus.Pending,
//            UserId = userId
//        };

//        _mockMapper.Setup(m => m.Map<TodoItem>(command))
//            .Returns(todoEntity);

//        _mockUnitOfWork.Setup(u => u.WriteRepository<TodoItem>().AddAsync(It.IsAny<TodoItem>()))
//            .ReturnsAsync(todoEntity);

//        _mockUnitOfWork.Setup(u => u.SaveChangesAsync())
//            .ReturnsAsync(1);

//        _mockUnitOfWork.Setup(u => u.CommitTransactionAsync())
//            .Returns(Task.CompletedTask);

//        _mockMapper.Setup(m => m.Map<TodoItemListDto>(It.IsAny<TodoItem>()))
//            .Returns(new TodoItemListDto
//            {
//                ID = 1,
//                Title = "Test Task Title",
//                Description = "This is a description for test task.",
//                DueDate = dueDate,
//                Status = TodoStatus.Pending,
//                UserId = userId
//            });

//        // Act
//        var result = await _handler.Handle(command, CancellationToken.None);

//        // Assert
//        Assert.NotNull(result);
//        Assert.True(result.IsSuccess, "Expected success result but got failure");
//        _mockUnitOfWork.Verify(u => u.BeginTransaction(), Times.Once);
//        _mockUnitOfWork.Verify(u => u.SaveChangesAsync(), Times.Once);
//        _mockUnitOfWork.Verify(u => u.CommitTransactionAsync(), Times.Once);
//    }


//    [Fact]
//    public async Task Handle_InvalidRequest_ReturnsValidationError()
//    {
//        // Arrange
//        var command = new CreateTodoItemCommand
//           (
//            "Title",                // title
//            "Description",         // description
//            DateTime.Now,         // dueDate
//            TodoStatus.Pending,  // status (enum)
//            Guid.NewGuid(),     // userId
//            "Some string",     // another string param
//            new CommandInfo() // command info class
//           );

//        var failures = new List<FluentValidation.Results.ValidationFailure>
//        {
//            new FluentValidation.Results.ValidationFailure("Property", "Error message")
//        };
//        _mockValidator.Setup(v => v.Validate(command))
//            .Returns(new FluentValidation.Results.ValidationResult(failures));

//        // Act
//        var result = await _handler.Handle(command, CancellationToken.None);

//        // Assert
//        Assert.False(result.IsSuccess);
//        Assert.Equal(ResponseTypes.ValidationError, result.ResponseType);
//        _mockUnitOfWork.Verify(u => u.BeginTransaction(), Times.Never);
//    }
//}

