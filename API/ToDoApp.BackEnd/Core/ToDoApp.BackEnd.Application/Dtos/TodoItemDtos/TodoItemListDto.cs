using ToDoApp.BackEnd.Application.Dtos.CommonDtos;
using ToDoApp.BackEnd.Domain.Enums;

namespace ToDoApp.BackEnd.Application.Dtos.TodoItemDtos;
public record TodoItemListDto : BaseDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TodoStatus Status { get; set; }
    public Guid UserId { get; set; }
}
