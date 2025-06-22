namespace ToDoApp.BackEnd.API.ApiDtos.EmailControllerDto
{
    public class UpdateEmailTemplateDto : AddEmailTemplateDto
    {
        public int Id { get; set; }
        public bool RowStatus { get; set; }
    }
}
