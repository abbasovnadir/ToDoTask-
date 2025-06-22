using ToDoApp.BackEnd.Application.Dtos.CommonDtos;

namespace ToDoApp.BackEnd.Application.Dtos.ApplicationDtos
{
    public record EmailTemplateListDto : BaseDto
    {
        public int Type { get; set; }
        public string TypeName { get; set; }
        public string Template { get; set; }
    }
}
