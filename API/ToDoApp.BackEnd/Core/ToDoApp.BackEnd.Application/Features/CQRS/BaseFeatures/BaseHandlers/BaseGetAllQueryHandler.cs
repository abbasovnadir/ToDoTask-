using AutoMapper;
using ToDoApp.BackEnd.Application.Dtos.CommonDtos;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.SuccessResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Domain.Entities.Common;
using MediatR;
using System.Linq.Expressions;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
public abstract class BaseGetAllQueryHandler<TRequest, TEntity , TResponse> : IRequestHandler<TRequest, IDataResult<IEnumerable<TResponse>>>
    where TRequest : IRequest<IDataResult<IEnumerable<TResponse>>>, IGetAllQuery
    where TResponse : BaseDto, new()
    where TEntity : BaseEntity, new()
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    protected BaseGetAllQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IDataResult<IEnumerable<TResponse>>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            Expression<Func<TEntity, bool>> filter = GetFilter(request);

            var entities = await _uow.ReadRepository<TEntity>().GetAllAsync(filter);

            var dtos = _mapper.Map<IEnumerable<TResponse>>(entities);

            return new SuccessDataResult<IEnumerable<TResponse>>(dtos, ResponseTypes.Success);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<IEnumerable<TResponse>>(ResponseTypes.Exception, $"Error: {ex.Message}");
        }
    }

    protected virtual Expression<Func<TEntity, bool>> GetFilter(TRequest request)
    {
        if (request.DataResponseType == DataResponseTypes.All)
            return x => x.RowStatus || !x.RowStatus;
        else if (request.DataResponseType == DataResponseTypes.IsActive)
            return x => x.RowStatus;
        else
            return x => !x.RowStatus;
    }
}
