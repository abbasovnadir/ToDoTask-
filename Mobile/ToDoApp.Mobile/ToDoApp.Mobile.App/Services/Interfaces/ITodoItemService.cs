using ToDoApp.Mobile.App.Models;
using ToDoApp.Mobile.App.Models.Enums;

namespace ToDoApp.Mobile.App.Services.Interfaces;
public interface ITodoItemService
{
    Task<List<TodoItemListResponse>> GetAll();
    Task<List<TodoItemListResponse>> GetByFilter(TodoStatus todoStatus);
    Task<TodoItemListResponse> GetById(int id);
    Task<bool> Create(TodoItemCreateRequest todoItem);
    Task<bool> UpdateStatus(int id, TodoStatus Status);
    Task<TodoItemListResponse> Update(TodoItemUpdateRequest todoItem);
    Task<bool> Delete(int id);
}
