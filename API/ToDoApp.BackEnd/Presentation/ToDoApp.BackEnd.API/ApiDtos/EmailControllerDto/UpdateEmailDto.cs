namespace ToDoApp.BackEnd.API.ApiDtos.EmailControllerDto
{
    public class UpdateEmailDto : AddEmailDto
    {
        public int Id { get; set; }
        public bool EmailStatus { get; set; }
    }
}
