using ToDoApp.BackEnd.Domain.Enums;

namespace ToDoApp.BackEnd.API.ApiDtos.ToDoItems
{
    public class TodoItemUpdateRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public TodoStatus Status { get; set; }
        public bool Rowstatus { get; set; }
    }
}
