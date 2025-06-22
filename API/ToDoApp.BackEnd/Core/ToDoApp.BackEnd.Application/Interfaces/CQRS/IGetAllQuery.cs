using ToDoApp.BackEnd.Application.Utilities.Enums;
using ICommand = ToDoApp.BackEnd.Application.Interfaces.CQRS.Common.ICommand;

namespace ToDoApp.BackEnd.Application.Interfaces.CQRS;
public interface IGetAllQuery : ICommand
{
    public DataResponseTypes DataResponseType { get; set; }
}
