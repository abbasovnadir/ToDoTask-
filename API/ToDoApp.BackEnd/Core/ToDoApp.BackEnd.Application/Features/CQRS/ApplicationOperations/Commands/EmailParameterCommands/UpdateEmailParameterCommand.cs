using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseCommands;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Commands.EmailParameterCommands;
public sealed class UpdateEmailParameterCommand : UpdateCommandBase<IDataResult<EmailParameterListDto>>, IUpdateCommand
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string SMTP { get; set; }
    public int Port { get; set; }
    public bool SSL { get; set; }
    public int EmilParameterType { get; set; }
    public UpdateEmailParameterCommand(int id, string email, string password, string smtp, int port, bool ssl, int emilParameterType, string updatedUser,bool rowStatus) : base(id, updatedUser, rowStatus)
    {
        Email = email;
        Password = password;
        SMTP = smtp;
        Port = port;
        SSL = ssl;
        EmilParameterType = emilParameterType;
    }
}
