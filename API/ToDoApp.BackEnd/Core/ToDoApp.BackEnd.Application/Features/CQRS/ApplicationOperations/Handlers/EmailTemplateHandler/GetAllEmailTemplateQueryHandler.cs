using AutoMapper;
using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Queries.EmailTemplateQueries;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Handlers.EmailTemplateHandler;
internal sealed class GetAllEmailTemplateQueryHandler : BaseGetAllQueryHandler<GetAllEmailTemplateQuery, EmailTemplate, EmailTemplateListDto>
{
    public GetAllEmailTemplateQueryHandler(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
    {
    }
}
