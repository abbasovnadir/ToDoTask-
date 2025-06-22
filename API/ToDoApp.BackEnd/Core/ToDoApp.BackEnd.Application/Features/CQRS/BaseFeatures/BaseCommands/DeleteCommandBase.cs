using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using MediatR;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseCommands;
public class DeleteCommandBase<T> : IRequest<T> where T : IResult
{
    public virtual int ID { get; set; }

    public DeleteCommandBase(int id)
    {
        ID = id;
    }
}
