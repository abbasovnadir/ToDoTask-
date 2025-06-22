namespace ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
public interface IDataResult<out T> : IResult
{
    T Data { get; }
}
