using ToDoApp.BackEnd.Application.Objects.Concrete;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Utilities.Results.Concretes;

public class Result : IResult
{
    public Result(ResponseTypes responseTypes, bool success)
    {
        ResponseType = responseTypes;
        IsSuccess = success;
    }
    public Result(ResponseTypes responseTypes,bool success, string message) : this(responseTypes,success)
    {
        Message = message;
    }

    public Result(ResponseTypes responseTypes, bool success, List<CustomValidationError> customValidationErrors) : this(responseTypes, success)
    {
        CustomValidationErrors = customValidationErrors;
    }
    public bool IsSuccess { get; }

    public string Message { get; }

    public ResponseTypes ResponseType  { get; }
    public List<CustomValidationError> CustomValidationErrors { get; set ; }
}
