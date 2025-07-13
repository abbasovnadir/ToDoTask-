using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseQueries;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Features.CQRS.ApplicationOperationFeatures.Queries.EmailParameterQueries;
public sealed class GetAllEmailParameterQuery : GetQueryBase<IDataResult<IEnumerable<EmailParameterListDto>>>, IGetAllQuery
{
    public GetAllEmailParameterQuery(DataResponseTypes dataResponseType) : base(dataResponseType)
    {
    }
}
