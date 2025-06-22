using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseQueries;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;
using System.Linq.Expressions;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperations.Queries.EmailTemplateQueries;
public sealed class GetByFilterEmailTemplateQuery : GetByFilterBase<EmailTemplate, EmailTemplateListDto>
{
    public GetByFilterEmailTemplateQuery(Expression<Func<EmailTemplate, bool>> filter) : base(filter)
    {
    }
}
