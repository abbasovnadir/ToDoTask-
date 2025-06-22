using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using MediatR;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers.ExceptionHandler;
public abstract class BaseCommandExceptionHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
     where TRequest : IRequest<TResponse>
     where TResponse : IResult
{
    protected virtual IResult HandleError(Exception ex)
    {
        if (ex is OperationCanceledException)
        {
            return new ErrorResult(ResponseTypes.Cancelled, "Operation was cancelled.");
        }
        return new ErrorResult(ResponseTypes.Exception, ex.Message);
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
