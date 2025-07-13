using AutoMapper;
using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Queries.EmailParameterQueries;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Handlers.EmailParameterHandlers;
internal class GetAllEmailParameterQueryHandler : BaseGetAllQueryHandler<GetAllEmailParameterQuery, EmailParameter, EmailParameterListDto>
{
    public GetAllEmailParameterQueryHandler(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
    {
    }
}
