using AutoMapper;
using FluentValidation;
using ToDoApp.BackEnd.Application.Dtos.TodoItemDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Commands;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Domain.Entities;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Handlers;

internal sealed class CreateTodoItemCommandHandler : BaseCreateCommandHandler<CreateTodoItemCommand, TodoItem, TodoItemListDto>
{
    public CreateTodoItemCommandHandler(IUnitOfWork uow, IValidator<CreateTodoItemCommand> validator, IMapper mapper) : base(uow, validator, mapper)
    {
    }

}