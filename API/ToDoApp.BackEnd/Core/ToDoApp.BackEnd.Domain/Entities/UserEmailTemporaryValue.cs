using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Domain.Entities.UserLoginEntities
{
    public class UserEmailTemporaryValue : UserEntity
    {
        public Guid Id { get; set; }
        public string ConfirmValue { get; set; }
        public int ConfirmValueType { get; set; }
        public DateTime ValueExpiredDate { get; set; }
        public bool IsConfirmed { get; set; }
        public AppUser AppUser { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
