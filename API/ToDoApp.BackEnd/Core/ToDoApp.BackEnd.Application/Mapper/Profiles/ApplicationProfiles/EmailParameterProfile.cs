using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Commands.EmailParameterCommands;
using ToDoApp.BackEnd.Application.Mapper.Profiles.Common;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;

namespace ToDoApp.BackEnd.Application.Mapper.Profiles.ApplicationProfiles;

public sealed class EmailParameterProfile : BaseProfile<EmailParameter, EmailParameterListDto>
{
    public EmailParameterProfile()
    {
        CreateMap<EmailParameter, CreateEmailParameterCommand>().ReverseMap();
        CreateMap<EmailParameter, UpdateEmailParameterCommand>().ReverseMap();
        CreateMap<EmailParameter, EmailParametersDto>().ReverseMap();

    }
}
