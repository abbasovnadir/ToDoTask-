namespace ToDoApp.BackEnd.Domain.Entities.Common;
public interface IHasUpdatedInfo
{
    DateTime? UpdatedAt { get; set; }
    string UpdatedUser { get; set; }
}