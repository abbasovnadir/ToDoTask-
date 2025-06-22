using ToDoApp.BackEnd.Application.Utilities.Enums;

namespace ToDoApp.BackEnd.Application.Utilities.Results.Concretes.SuccessResults;
public class SuccessResult : Result
{
    public SuccessResult(ResponseTypes responseTypes) : base(responseTypes,true)
    {
    }

    public SuccessResult(ResponseTypes responseTypes,string message) : base(responseTypes,true, message)
    {
    }
}
