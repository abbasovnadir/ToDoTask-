using ToDoApp.BackEnd.Application.Dtos.CommonDtos.Interfaces;

namespace ToDoApp.BackEnd.Application.Dtos.UserLoginDtos;
public class UserForLoginDto : IDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
