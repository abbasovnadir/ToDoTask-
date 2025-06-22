using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Domain.Entities;
public class AppRole : UserEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }

    public ICollection<AppUserRoles> AppUserRoles { get; set; }
}
