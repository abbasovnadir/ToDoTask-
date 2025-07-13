using AutoMapper;
using FluentValidation;
using MediatR;
using ToDoApp.BackEnd.Application.Dtos.CommonDtos;
using ToDoApp.BackEnd.Application.Extensions.FluentExtensions;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.SuccessResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;


public abstract class BaseCreateCommandHandler<TRequest, TEntity, TResponse> :
    BaseDataCommandHandler<TRequest, TResponse>
    where TRequest : IRequest<IDataResult<TResponse>>
    where TEntity : BaseEntity, new()
    where TResponse : BaseDto, new()
{
    public readonly IUnitOfWork _uow;
    private readonly IValidator<TRequest> _validator;
    private readonly IMapper _mapper;

    protected BaseCreateCommandHandler(IUnitOfWork unitOfWork, IValidator<TRequest> validator, IMapper mapper)
    {
        _uow = unitOfWork;
        _validator = validator;
        _mapper = mapper;
    }

    public override async Task<IDataResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var validate = _validator.Validate(request);
            if (!validate.IsValid)
                return new ErrorDataResult<TResponse>(ResponseTypes.ValidationError, validate.ConvertToCustomValidationError());

            cancellationToken.ThrowIfCancellationRequested();

            var entity = _mapper.Map<TEntity>(request);
            _uow.BeginTransaction();

            TResponse dto = null;

            return await TryWithRollbackAsync(async () =>
            {
                var data = await _uow.WriteRepository<TEntity>().AddAsync(entity);
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();

                dto = _mapper.Map<TResponse>(data);
                return new SuccessDataResult<TResponse>(dto, ResponseTypes.Success);

            }, async () =>
            {
                await _uow.RollbackTransactionAsync();

                if (dto is not null && dto.ID > 0)
                {
                    var getData = await _uow.ReadRepository<TEntity>().GetFilterAsync(x => x.ID == dto.ID);
                    _uow.WriteRepository<TEntity>().DeleteAsync(getData);
                    await _uow.SaveChangesAsync();
                }
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
    //        //if(await CheckDublicate(request))
    //        //    return new ErrorDataResult<TResponse>(ResponseTypes.ExistData);

    //        cancellationToken.ThrowIfCancellationRequested();

    //        var entity = _mapper.Map<TEntity>(request);

    //        _uow.BeginTransaction();
    //        TResponse dto = null;
    //        try
    //        {
    //            var data = await _uow.WriteRepository<TEntity>().AddAsync(entity);
    //            await _uow.SaveChangesAsync();

    //            await _uow.CommitTransactionAsync();

    //            dto = _mapper.Map<TResponse>(data);
    //        }
    //        catch (Exception ex)
    //        {
    //            await _uow.RollbackTransactionAsync();

    //            if (dto is not null && dto.ID > 0)
    //            {
    //                var getData = await _uow.ReadRepository<TEntity>().GetFilterAsync(x=>x.ID == dto.ID);
    //                 _uow.WriteRepository<TEntity>().DeleteAsync(getData);
    //                await _uow.SaveChangesAsync();
    //            }
    //            return HandleError(ex);
    //        }

    //        return new SuccessDataResult<TResponse>(dto, ResponseTypes.Success);
    //    }
    //    catch (Exception ex)
    //    {
    //        await _uow.RollbackTransactionAsync();
    //        return HandleError(ex);
    //    }
    //}


    //protected virtual async Task<bool> CheckDublicate(TRequest request)
    //{
    //    return false; 
    //}