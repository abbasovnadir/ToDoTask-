using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Commands.EmailTemplateCommands;
using ToDoApp.BackEnd.Application.Mapper.Profiles.Common;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;

namespace ToDoApp.BackEnd.Application.Mapper.Profiles.ApplicationProfiles;
public class EmailTemplateProfile : BaseProfile<EmailTemplate,EmailTemplateListDto>
{
    public EmailTemplateProfile()
    {
        CreateMap<EmailTemplate, CreateEmailTemplateCommand>().ReverseMap();
        CreateMap<EmailTemplate, UpdateEmailTemplateCommand>().ReverseMap();
    }
}
