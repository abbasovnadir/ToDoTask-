using ToDoApp.BackEnd.Application.Dtos.TodoItemDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseQueries;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Queries;
public class GetAllTodoItemQuery : GetQueryBase<IDataResult<IEnumerable<TodoItemListDto>>>, IGetAllQuery
{
    public GetAllTodoItemQuery(int id, DataResponseTypes dataResponseType) : base(id, dataResponseType)
    {
    }
}
