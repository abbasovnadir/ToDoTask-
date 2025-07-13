using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseCommands;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailParameterCommands;
public sealed class CreateEmailParameterCommand : CreateCommandBase<IDataResult<EmailParameterListDto>>
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string SMTP { get; set; }
    public int Port { get; set; }
    public bool SSL { get; set; }
    public int EmailParameterType { get; set; }

    public CreateEmailParameterCommand(string email, string password, string smtp, int port, bool ssl, int emilParameterType, string createdUser) : base(createdUser, CommandInfo.RowStatusIsActive)
    {
        Email = email;
        Password = password;
        SMTP = smtp;
        Port = port;
        SSL = ssl;
        EmailParameterType = emilParameterType;
    }
}
