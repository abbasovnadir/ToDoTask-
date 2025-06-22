using ToDoApp.Mobile.App.Models.Enums;

namespace ToDoApp.Mobile.App.Models;
public class TodoItemListResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TodoStatus Status { get; set; }
    public Guid UserId { get; set; }
}
