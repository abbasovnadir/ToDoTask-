using ToDoApp.Mobile.App.Models;

namespace ToDoApp.Mobile.App.Services.Interfaces;

public interface IAuthenticationService
{
    Task<UserSession> GetUserSession();
    Task SaveUserSession(UserSession userSession);
    void RemoveUserSession();
}
