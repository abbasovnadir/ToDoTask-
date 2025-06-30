namespace ToDoApp.Mobile.App.Models;
public class UserSession
{
    public UserSession(string userId,string userName,string email, string memberSince, List<string> roles, string accessToken)
    {
        UserName = userName;
        Email = email;
        MemberSince = memberSince;
        Roles = roles;
        AccessToken = accessToken;
        UserId = userId;
    }

    public string UserId { get; set; }
    public string UserName { get; set; }
    public List<string> Roles { get; set; }
    public string AccessToken { get; set; }
    public string Email { get; set; }
    public string MemberSince { get; set; }



}
