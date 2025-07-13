using MediatR;
using ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers.ExceptionHandler;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers;
public abstract class BaseDataCommandHandler<TRequest, TResponse> : BaseCommandExceptionHandler<TRequest, IDataResult<TResponse>>
where TRequest : IRequest<IDataResult<TResponse>>
{
    public abstract override Task<IDataResult<TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    public static IDataResult<T> HandleException<T>(Exception ex)
    {
        if (ex is OperationCanceledException)
            return new ErrorDataResult<T>(ResponseTypes.Cancelled, "Operation was cancelled.");

        return new ErrorDataResult<T>(ResponseTypes.Exception, ex.Message);
    }
}
