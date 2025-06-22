using AutoMapper;

namespace ToDoApp.BackEnd.Application.Mapper.Profiles.Common;
public abstract class BaseProfile<TSource, TDestination> : Profile
{
    protected BaseProfile()
    {
        CreateMap<TSource, TDestination>().ReverseMap();
    }
}
