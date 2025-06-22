using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Domain.Entities;

public class AppUserRoles : UserEntity
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public AppUser AppUser { get; set; }
    public AppRole AppRole { get; set; }
}