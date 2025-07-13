using AutoMapper;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.SuccessResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Domain.Entities.Common;
using FluentValidation;
using MediatR;
using ToDoApp.BackEnd.Application.Extensions.FluentExtensions;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;

public abstract class BaseUpdateCommandHandler<TRequest, TEntity, TResponse> : BaseDataCommandHandler<TRequest, TResponse>
    where TEntity : BaseEntity, new()
    where TRequest : IRequest<IDataResult<TResponse>>, IUpdateCommand
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly IValidator<TRequest> _validator;
    protected BaseUpdateCommandHandler(IUnitOfWork uow, IMapper mapper, IValidator<TRequest> validator)
    {
        _uow = uow;
        _mapper = mapper;
        _validator = validator;
    }

    public override async Task<IDataResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validate = _validator.Validate(request);
            if (!validate.IsValid)
                return new ErrorDataResult<TResponse>(ResponseTypes.ValidationError, validate.ConvertToCustomValidationError());

            cancellationToken.ThrowIfCancellationRequested();

            _uow.BeginTransaction();

            return await TryWithRollbackAsync(async () =>
            {
                var entity = await _uow.ReadRepository<TEntity>().FindAsync(x => x.ID == request.ID);
                if (entity == null)
                {
                    return new ErrorDataResult<TResponse>(ResponseTypes.NotFound, $"Entity with ID {request.ID} not found.");
                }

                var updatedEntity = _mapper.Map(request, entity);
                updatedEntity.ID = request.ID;

                _uow.WriteRepository<TEntity>().Update(x => x.ID == entity.ID, updatedEntity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                var dto = _mapper.Map<TResponse>(updatedEntity);
                return new SuccessDataResult<TResponse>(dto, ResponseTypes.Success);

            });
        }
        catch (Exception ex)
        {
            await _uow.RollbackTransactionAsync();
            return HandleException<TResponse>(ex);
        }
    }
}



    //public override async Task<IDataResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    //{
    //    try
    //    {
    //        var validate = _validator.Validate(request);
    //        if (!validate.IsValid)
    //            return new ErrorDataResult<TResponse>(ResponseTypes.ValidationError, validate.ConvertToCustomValidationError());

    //        var entity = await _uow.ReadRepository<TEntity>().FindAsync(x => x.ID == request.ID);
    //        if (entity == null)
    //        {
    //            return new ErrorDataResult<TResponse>(ResponseTypes.NotFound, $"Entity with ID {request.ID} not found.");
    //        }

    //        // Mapping and updating fields
    //        var updatedEntity = _mapper.Map(request, entity); // This will map only the updated fields
    //        updatedEntity.ID = request.ID;
    //        _uow.WriteRepository<TEntity>().Update(x => x.ID == entity.ID, updatedEntity);
    //        await _uow.SaveChangesAsync();

    //        var dto = _mapper.Map<TResponse>(updatedEntity);
    //        return new SuccessDataResult<TResponse>(dto, ResponseTypes.Success);
    //    }
    //    catch (Exception ex)
    //    {
    //        return HandleError(ex);
    //    }
    //}

