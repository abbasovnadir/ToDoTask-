using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Domain.Entities;
public class AppUserToken : UserEntity
{
    public int Id { get; set; }
    public AppUser AppUser { get; set; }
    public Guid UserId { get; set; }
    public string RefreshToken { get; set; }
    public DateTime TokenExpiredDate { get; set; }
    public DateTime CreatedAt { get; set; }
}