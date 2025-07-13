using ToDoApp.BackEnd.Application.Objects.Concrete;
using ToDoApp.BackEnd.Application.Utilities.Enums;

namespace ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
public interface IResult
{
    public ResponseTypes ResponseType { get; }
    public bool IsSuccess { get; }
    public string Message { get; }

    public List<CustomValidationError> CustomValidationErrors { get; set;}
}
