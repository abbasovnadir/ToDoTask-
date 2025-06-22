using ToDoApp.BackEnd.Application.Dtos.CommonDtos.Interfaces;

namespace ToDoApp.BackEnd.Application.Dtos.UserLoginDtos;
public class AppUserCreateDto : IDto
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
