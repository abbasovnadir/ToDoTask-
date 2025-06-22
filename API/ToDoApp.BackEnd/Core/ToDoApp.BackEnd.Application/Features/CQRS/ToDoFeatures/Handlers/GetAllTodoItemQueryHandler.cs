using AutoMapper;
using ToDoApp.BackEnd.Application.Dtos.TodoItemDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Queries;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Domain.Entities;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Handlers;
internal sealed class GetAllTodoItemQueryHandler : BaseGetAllQueryHandler<GetAllTodoItemQuery, TodoItem, TodoItemListDto>
{
    public GetAllTodoItemQueryHandler(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
    {
    }
}
