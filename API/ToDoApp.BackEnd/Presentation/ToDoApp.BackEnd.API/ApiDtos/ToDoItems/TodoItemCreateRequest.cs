using ToDoApp.BackEnd.Domain.Enums;

namespace ToDoApp.BackEnd.API.ApiDtos.ToDoItems
{
    public class TodoItemCreateRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TodoStatus Status { get; set; }
    }
}
