using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using MediatR;

namespace ToDoApp.BackEnd.Application.Features.CQRS.BaseFeatures.BaseQueries
{
    public abstract class GetQueryBase<T> : IRequest<T> where T : IResult
    {
        public virtual int ID { get; set; }
        public virtual DataResponseTypes DataResponseType { get; set; }


        protected GetQueryBase(int id)
        {
            ID = id;
        }

        protected GetQueryBase(DataResponseTypes dataResponseType)
        {
            DataResponseType = dataResponseType;
        }

        protected GetQueryBase(int id, DataResponseTypes dataResponseType) : this(id)
        {           
            DataResponseType = dataResponseType;
        }
    }
}
