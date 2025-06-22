using AutoMapper;
using ToDoApp.BackEnd.Application.Dtos.CommonDtos;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.SuccessResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Domain.Entities.Common;
using MediatR;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;

public abstract class BaseGetByIdQueryHandler<TRequest, TResponse, TEntity> : IRequestHandler<TRequest, IDataResult<TResponse>>
where TRequest : IRequest<IDataResult<TResponse>>, IGetByIdQuery
where TResponse : BaseDto, new()
where TEntity : BaseEntity, new()
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    protected BaseGetByIdQueryHandler(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<IDataResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var entities = await _uow.ReadRepository<TEntity>().GetFilterAsync(x=>x.ID == request.ID);

            var dtos = _mapper.Map<TResponse>(entities);

            return new SuccessDataResult<TResponse>(dtos, Utilities.Enums.ResponseTypes.Success);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<TResponse>(Utilities.Enums.ResponseTypes.Exception, $"Error: {ex.Message}");
        }
    }
}