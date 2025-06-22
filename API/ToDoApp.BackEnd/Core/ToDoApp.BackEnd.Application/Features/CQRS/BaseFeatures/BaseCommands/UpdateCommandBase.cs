using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using MediatR;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseCommands;

public abstract class UpdateCommandBase<T> : IRequest<T> where T : IResult
{
    public virtual int ID { get; set; }
    public virtual DateTime UpdatedAt { get; set; }
    public virtual string UpdatedUser { get; set; }
    public virtual bool RowStatus { get; set; }

    public UpdateCommandBase(int id,  string updatedUser)
    {
        ID = id;
        UpdatedAt = DateTime.Now;
        UpdatedUser = updatedUser;
    }

    public UpdateCommandBase(int id,string updatedUser, bool rowStatus): this(id, updatedUser)
    {
        RowStatus = rowStatus;
    }
}
