using ToDoApp.BackEnd.Application.Utilities.Enums;
using  ToDoApp.BackEnd.Application.Interfaces.CQRS.Common;

namespace ToDoApp.BackEnd.Application.Interfaces.CQRS;
public interface IGetAllQuery : ICommand
{
    public DataResponseTypes DataResponseType { get; set; }
}
