using ToDoApp.BackEnd.Domain.Entities.Common;
using ToDoApp.BackEnd.Domain.Enums;

namespace ToDoApp.BackEnd.Domain.Entities;
public class TodoItem : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TodoStatus Status { get; set; }
    public Guid UserId { get; set; }
    public AppUser AppUser { get; set; }

}
