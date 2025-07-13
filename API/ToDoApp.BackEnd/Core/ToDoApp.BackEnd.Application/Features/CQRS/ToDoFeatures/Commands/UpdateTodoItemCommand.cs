using ToDoApp.BackEnd.Application.Dtos.TodoItemDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseCommands;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Domain.Enums;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Commands;
public class UpdateTodoItemCommand : UpdateCommandBase<IDataResult<TodoItemListDto>>, IUpdateCommand
{
    public UpdateTodoItemCommand(int id, string title, string description, DateTime dueDate,
                                TodoStatus status, Guid userId, string updatedUser, bool rowStatus) 
                                : base(id, updatedUser, rowStatus)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Status = status;
        UserId = userId;
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TodoStatus Status { get; set; }
    public Guid UserId { get; set; }
}
