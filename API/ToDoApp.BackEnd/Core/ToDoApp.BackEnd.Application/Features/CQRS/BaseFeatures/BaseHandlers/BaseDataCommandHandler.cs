using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers.ExceptionHandler;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using MediatR;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
public abstract class BaseDataCommandHandler<TRequest, TResponse> : BaseCommandExceptionHandler<TRequest, IDataResult<TResponse>>
where TRequest : IRequest<IDataResult<TResponse>>
{
    public abstract override Task<IDataResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);

    protected override IDataResult<TResponse> HandleError(Exception ex)
    {
        if (ex is OperationCanceledException)
        {
            return new ErrorDataResult<TResponse>(ResponseTypes.Cancelled, "Operation was cancelled.");
        }
        return new ErrorDataResult<TResponse>(ResponseTypes.Exception, ex.Message);
    }
}
