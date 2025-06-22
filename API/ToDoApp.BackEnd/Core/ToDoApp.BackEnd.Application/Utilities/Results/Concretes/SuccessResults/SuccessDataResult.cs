using ToDoApp.BackEnd.Application.Utilities.Enums;

namespace ToDoApp.BackEnd.Application.Utilities.Results.Concretes.SuccessResults;
public class SuccessDataResult<T> : DataResult<T>
{
    public SuccessDataResult(T data, ResponseTypes responseTypes) : base(data, responseTypes, true)
    {
    }

    public SuccessDataResult(T data, ResponseTypes responseTypes, string message) : base(data, responseTypes, true, message)
    {
    }
    public SuccessDataResult(ResponseTypes responseTypes) : base(default, responseTypes, true)
    {
    }
}
