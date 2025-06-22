using ToDoApp.BackEnd.Application.Utilities.Enums;

namespace ToDoApp.BackEnd.API.ApiDtos.EmailControllerDto
{
    public class AddEmailTemplateDto
    {
        public EmailTemplateTypes Type { get; set; }
        public string Template { get; set; }
        public string CreatedUser { get; set; }
    }
}
