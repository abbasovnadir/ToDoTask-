namespace ToDoApp.BackEnd.API.ApiDtos.EmailControllerDto
{
    public class AddEmailDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
        public int EmailParameterType { get; set; }
        public string CreatedUser { get; set; }
    }
}
