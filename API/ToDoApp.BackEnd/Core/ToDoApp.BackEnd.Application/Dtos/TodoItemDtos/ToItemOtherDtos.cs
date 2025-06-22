using ToDoApp.BackEnd.Domain.Enums;

namespace ToDoApp.BackEnd.Application.Dtos.TodoItemDtos;

public record TodoItemCreateDto(string Title, string Description, DateTime DueDate, TodoStatus Status, Guid UserId);
public record TodoItemUpdateDto(int ID, string Title, string Description, DateTime DueDate, TodoStatus Status, Guid UserId);

