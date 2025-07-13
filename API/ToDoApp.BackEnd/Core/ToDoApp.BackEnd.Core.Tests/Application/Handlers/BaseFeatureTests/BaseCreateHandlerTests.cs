using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Moq;
using ToDoApp.BackEnd.Application.Dtos.CommonDtos;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Core.Tests.Application.Handlers.BaseFeatureTests
{
    public abstract class BaseCreateHandlerTests<TCommand, TEntity, TResponse>
        where TCommand  : IRequest<IDataResult<TResponse>>
        where TEntity : BaseEntity, new()
        where TResponse : BaseDto, new()
    {
        protected Mock<IUnitOfWork> MockUow = new();
        protected Mock<IValidator<TCommand>> MockValidator =  new();
        protected Mock<IMapper> MockMapper = new();

        protected abstract TCommand CreateValidCommand();
        protected abstract TEntity CreateEntity();
        protected abstract TResponse CreateResponse();

        protected abstract IRequestHandler<TCommand, IDataResult<TResponse>> CreateHandler();


        [Fact]
        public async Task Handle_ValidRequest_ReturnsSuccess()
        {
            // Arrange
            var command = CreateValidCommand();
            var entity = CreateEntity();
            var responseDto = CreateResponse();

            MockValidator.Setup(v => v.Validate(command)).Returns(new ValidationResult());

            MockMapper.Setup(m => m.Map<TEntity>(command)).Returns(entity);
            MockUow.Setup(u => u.WriteRepository<TEntity>().AddAsync(entity)).ReturnsAsync(entity);
            MockUow.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);
            MockUow.Setup(u => u.CommitTransactionAsync()).Returns(Task.CompletedTask);
            MockMapper.Setup(m => m.Map<TResponse>(entity)).Returns(responseDto);

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(ResponseTypes.Success, result.ResponseType);
        }

        [Fact]
        public async Task Handle_InvalidRequest_ReturnsValidationError()
        {
            // Arrange
            var command = CreateValidCommand();

            var failures = new List<ValidationFailure>
            {
                new ValidationFailure("Property", "Error message")
            };

            MockValidator.Setup(v => v.Validate(command)).Returns(new  ValidationResult(failures));

            var handler = CreateHandler();

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(ResponseTypes.ValidationError, result.ResponseType);
        }
    }
}
