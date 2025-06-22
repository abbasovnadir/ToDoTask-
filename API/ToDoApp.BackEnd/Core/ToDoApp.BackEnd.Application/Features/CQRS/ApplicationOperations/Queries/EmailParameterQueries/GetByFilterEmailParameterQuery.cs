using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseQueries;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;
using System.Linq.Expressions;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Queries.EmailParameterQueries;
public sealed class GetByFilterEmailParameterQuery : GetByFilterBase<EmailParameter, EmailParameterListDto>
{
    public GetByFilterEmailParameterQuery(Expression<Func<EmailParameter, bool>> filter) : base(filter)
    {
    }
}
