using ToDoApp.BackEnd.Application.Objects.Concrete;
using ToDoApp.BackEnd.Application.Utilities.Enums;

namespace ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
public class ErrorResult : Result
{
    public ErrorResult(ResponseTypes responseTypes) : base(responseTypes,false)
    {
    }

    public ErrorResult(ResponseTypes responseTypes,string message) : base(responseTypes,false, message)
    {
    }

    public ErrorResult(ResponseTypes responseTypes, List<CustomValidationError> customValidationErrors) : base(responseTypes, false, customValidationErrors)
    {
    }
}
