namespace ToDoApp.BackEnd.Domain.Entities.Common;
public interface IHasCreatedInfo
{
    DateTime CreatedAt { get; set; }
    string CreatedUser { get; set; }
}
