namespace ToDoApp.BackEnd.API.ApiDtos.AuthControllerDtos;
public class ChangePasswordDto
{
    public string UserEmail { get; set; }
    public string NewPassword { get; set; }
    public string SecretKey { get; set; }
}
