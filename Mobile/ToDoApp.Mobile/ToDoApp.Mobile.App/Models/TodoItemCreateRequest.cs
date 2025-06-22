using ToDoApp.Mobile.App.Models.Enums;

namespace ToDoApp.Mobile.App.Models;
public class TodoItemCreateRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TodoStatus Status { get; set; }
}
