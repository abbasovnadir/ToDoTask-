using ToDoApp.BackEnd.Application.Dtos.CommonDtos.Interfaces;

namespace ToDoApp.BackEnd.Application.Dtos.UserLoginDtos;
public class AppUserDto : IDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public bool MailConfirm { get; set; }
    public string MailConfirmValue { get; set; }
    public string RoleName { get; set; }
}
