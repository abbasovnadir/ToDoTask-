using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseQueries;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Queries.EmailTemplateQueries;
public sealed class GetAllEmailTemplateQuery : GetQueryBase<IDataResult<IEnumerable<EmailTemplateListDto>>>, IGetAllQuery
{
    public GetAllEmailTemplateQuery(DataResponseTypes dataResponseType) : base(dataResponseType)
    {
    }
}
