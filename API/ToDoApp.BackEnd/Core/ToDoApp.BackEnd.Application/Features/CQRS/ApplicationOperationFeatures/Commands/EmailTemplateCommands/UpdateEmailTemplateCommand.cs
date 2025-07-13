using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseCommands;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailTemplateCommands;

public sealed class UpdateEmailTemplateCommand : UpdateCommandBase<IDataResult<EmailTemplateListDto>>, IUpdateCommand
{
    public int Type { get; set; }
    public string TypeName { get; set; }
    public string Template { get; set; }

    public UpdateEmailTemplateCommand(int id, int type, string template, string updatedUser, bool rowStatus) : base(id, updatedUser)
    {
        Type = type;
        TypeName = GetTypeNameByType(type);
        Template = template;
    }
    private string GetTypeNameByType(int type)
    {
        switch (type)
        {
            case (int)EmailTemplateTypes.Register:
                return "Register";
            case (int)EmailTemplateTypes.ChangePassword:
                return "ResetPassword";

            default:
                return "None";
        }
    }
}
