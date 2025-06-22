using ToDoApp.BackEnd.Application.Dtos.TodoItemDtos;
using ToDoApp.BackEnd.Application.Features.CQRS.ToDoFeatures.Commands;
using ToDoApp.BackEnd.Application.Mapper.Profiles.Common;
using ToDoApp.BackEnd.Domain.Entities;

namespace ToDoApp.BackEnd.Application.Mapper.Profiles.TodoItemProfiles
{
    public class TodoItemProfile : BaseProfile<TodoItem, TodoItemListDto>
    {
        public TodoItemProfile()
        {
            CreateMap<TodoItem, CreateTodoItemCommand>().ReverseMap();
            CreateMap<UpdateTodoItemCommand, TodoItem>().ForMember(dest => dest.ID, opt => opt.Ignore());

        }
    }
}
