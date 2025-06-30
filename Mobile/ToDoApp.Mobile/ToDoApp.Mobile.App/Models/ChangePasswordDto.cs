namespace ToDoApp.Mobile.App.Models;
public class ChangePasswordDto
{
    public string UserEmail { get; set; }
    public string OldPassword { get; set; }
    public string NewPassword { get; set; }
    public string SecretKey { get; set; }
}
