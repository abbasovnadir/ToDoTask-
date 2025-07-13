using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using MediatR;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseHandlers.ExceptionHandler;
public abstract class BaseCommandExceptionHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IResult
{
    protected virtual IResult HandleException(Exception ex)
    {
        if (ex is OperationCanceledException)
        {
            return new ErrorResult(ResponseTypes.Cancelled, "Operation was cancelled.");
        }
        return new ErrorResult(ResponseTypes.Exception, ex.Message);
    }
   

    protected async Task<TResponse> TryWithRollbackAsync(Func<Task<TResponse>> action, Func<Task> rollback = null)
    {
        try
        {
            return await action();
        }
        catch (Exception ex)
        {
            if (rollback is not null)
                await rollback();

            return (TResponse)HandleException(ex);
        }
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
