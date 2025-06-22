using ToDoApp.BackEnd.Application.Objects.Concrete;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;

namespace ToDoApp.BackEnd.Application.Utilities.Results.Concretes;
public class DataResult<T> : Result, IDataResult<T>
{
    public DataResult(T data, ResponseTypes responseTypes, bool success, string message) : base(responseTypes, success, message)
    {
        Data = data;
    }
    public DataResult(T data, ResponseTypes responseTypes, bool success) : base(responseTypes, success)
    {
        Data = data;
    }
    public DataResult(T data, ResponseTypes responseTypes, bool success, List<CustomValidationError> customValidationErrors) : base(responseTypes, success, customValidationErrors)
    {
        
    }
    public DataResult(ResponseTypes responseTypes, bool success, List<CustomValidationError> customValidationErrors) : base(responseTypes, success, customValidationErrors)
    {
        
    }
    public T Data { get; }
}