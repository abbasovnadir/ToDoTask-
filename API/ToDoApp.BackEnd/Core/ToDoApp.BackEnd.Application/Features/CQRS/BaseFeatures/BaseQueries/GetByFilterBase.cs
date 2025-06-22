using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseQueries;

public class GetByFilterBase<TEntity, TResponse> : IRequest<IDataResult<IEnumerable<TResponse>>>
{
    public Expression<Func<TEntity, bool>> Filter { get; set; }

    public GetByFilterBase(Expression<Func<TEntity, bool>> filter)
    {
        Filter = filter;
    }

}