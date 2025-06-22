using System.Linq.Expressions;
using ToDoApp.BackEnd.Application.Dtos.TodoItemDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseQueries;
using ToDoApp.BackEnd.Domain.Entities;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Queries;
public class GetByFilterIdTodoItemQuery : GetByFilterBase<TodoItem, TodoItemListDto>
{
    public GetByFilterIdTodoItemQuery(Expression<Func<TodoItem, bool>> filter) : base(filter)
    {
    }
}