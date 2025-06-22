using ToDoApp.BackEnd.Application.Objects.Concrete;
using ToDoApp.BackEnd.Application.Utilities.Enums;

namespace ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
public class ErrorDataResult<T> : DataResult<T>
{
    public ErrorDataResult(T data, ResponseTypes responseTypes) : base(data, responseTypes, false)
    {
    }

    public ErrorDataResult(T data, ResponseTypes responseTypes, string message) : base(data, responseTypes, false, message)
    {
    }
    public ErrorDataResult(ResponseTypes responseTypes) : base(default, responseTypes, false)
    {
    }

    public ErrorDataResult(ResponseTypes responseTypes,string message) : base(default, responseTypes, false, message)
    {
    }
    public ErrorDataResult(T data, ResponseTypes responseTypes, List<CustomValidationError> customValidationErrors) : base(data, responseTypes, false, customValidationErrors)
    {
    }
    public ErrorDataResult(ResponseTypes responseTypes, List<CustomValidationError> customValidationErrors) : base(responseTypes, false, customValidationErrors)
    {
    }
}
