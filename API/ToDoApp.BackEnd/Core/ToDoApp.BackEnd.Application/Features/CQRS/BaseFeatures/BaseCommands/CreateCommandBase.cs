using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using MediatR;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseCommands;
public abstract class CreateCommandBase<T> : IRequest<T> where T : IResult 
{
    public virtual DateTime CreatedAt { get; set; }
    public virtual string CreatedUser { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string UpdatedUser { get; set; }
    public virtual bool RowStatus { get; set; }

    protected CreateCommandBase()
    {
        CreatedAt = DateTime.Now;
        UpdatedAt = new DateTime(1900, 01, 01);
        UpdatedUser = string.Empty;
    }

    protected CreateCommandBase(string createdUser) : this()     
    {            
        CreatedUser = createdUser;
    }

    protected CreateCommandBase(string createdUser, CommandInfo rowStatus) : this(createdUser) 
    {
        RowStatus = rowStatus == CommandInfo.RowStatusIsActive ? true : false;
    }
}
