using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseCommands;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailTemplateCommands;
public sealed class CreateEmailTemplateCommand : CreateCommandBase<IDataResult<EmailTemplateListDto>>
{
    public int Type { get; set; }
    public string TypeName { get; set; }
    public string Template { get; set; }

    public CreateEmailTemplateCommand(int type, string template,string createdUser) : base(createdUser, CommandInfo.RowStatusIsActive)
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
                return "ChangePassword";
            case (int)EmailTemplateTypes.ResetPassword:
                return "ResetPassword";

            default:
                return "None";
        }
    }

}
