using ToDoApp.BackEnd.Application.Dtos.CommonDtos;

namespace ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;

public record EmailParameterListDto : BaseDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string SMTP { get; set; }
    public int Port { get; set; }
    public bool SSL { get; set; }
    public int EmailParameterType { get; set; }
}
