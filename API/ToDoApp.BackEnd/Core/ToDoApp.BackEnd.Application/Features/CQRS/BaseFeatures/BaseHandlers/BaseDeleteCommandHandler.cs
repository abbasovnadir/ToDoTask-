using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers.ExceptionHandler;
using ToDoApp.BackEnd.Application.Interfaces.CQRS;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.SuccessResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Domain.Entities.Common;
using MediatR;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
public abstract class BaseDeleteCommandHandler<TRequest,TEntity> : BaseCommandExceptionHandler<TRequest, IResult>
                        where TRequest : IRequest<IResult>, IDeleteCommand
                        where TEntity : BaseEntity, new()
{

    private readonly IUnitOfWork _uow;
    protected BaseDeleteCommandHandler(IUnitOfWork uow) => _uow = uow;

    public  override async Task<IResult> Handle(TRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.ID <= 0)
            {
                return new ErrorResult(ResponseTypes.ValidationError, $"ID = {request.ID}  cannot be empty.");
            }

            cancellationToken.ThrowIfCancellationRequested();
            _uow.BeginTransaction();
            try
            {
                var getEntity = await _uow.ReadRepository<TEntity>().GetFilterAsync(x => x.ID == request.ID);
                _uow.WriteRepository<TEntity>().DeleteAsync(getEntity);
                
                await _uow.SaveChangesAsync();
                await _uow.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _uow.RollbackTransactionAsync();
                return HandleError(ex);
            }
            return new SuccessResult(ResponseTypes.Success, "Successfully deleted");
        }
        catch (Exception ex)
        {
            await _uow.RollbackTransactionAsync();
            return HandleError(ex);
        }
    }
}
