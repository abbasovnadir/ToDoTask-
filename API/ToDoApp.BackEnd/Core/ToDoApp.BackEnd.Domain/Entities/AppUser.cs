using ToDoApp.BackEnd.Domain.Entities.Common;
using ToDoApp.BackEnd.Domain.Entities.UserLoginEntities;

namespace ToDoApp.BackEnd.Domain.Entities;

public class AppUser : UserEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public byte[] PaswordSalt { get; set; }
    public byte[] PaswordHash { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsActive { get; set; }
    public bool MailConfirm { get; set; }
    public string MailConfirmValue { get; set; }
    public DateTime MailConfirmDate { get; set; }
    public AppUserToken AppUserToken { get; set; }
    public ICollection<AppUserRoles> AppUserRoles { get; set; }
    public ICollection<TodoItem> TodoItems { get; set; }
    public UserEmailTemporaryValue UserEmailTemporaryValue { get; set; }
}
