using ToDoApp.BackEnd.Application.Dtos.TodoItemDtos;

namespace ToDoApp.BackEnd.API.ApiDtos.ToDoItems;

public class TodoStatusSummaryDto
{
    public IEnumerable<TodoItemListDto> Pending { get; set; }
    public IEnumerable<TodoItemListDto> InProgress { get; set; }
    public IEnumerable<TodoItemListDto> Completed { get; set; }
    public IEnumerable<TodoItemListDto> Canceled { get; set; }
    public IEnumerable<TodoItemListDto> Archived { get; set; }
}