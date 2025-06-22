using System.Text.Json;
using ToDoApp.Mobile.App.Models;
using ToDoApp.Mobile.App.Services.Interfaces;

namespace ToDoApp.Mobile.App.Services.Concrete
{
    public class AuthenticationService : IAuthenticationService
    {
        private const string USER_SESSION_KEY = "app_user_session";

        public async Task<UserSession> GetUserSession()
        {
            UserSession userSession = null;
            var userSessionJson = await SecureStorage.GetAsync(USER_SESSION_KEY);
            if (!string.IsNullOrEmpty(userSessionJson))
                userSession = JsonSerializer.Deserialize<UserSession>(userSessionJson);

            return userSession;
        }

        public async Task SaveUserSession(UserSession userSession)
        {
            var userSessionJson = JsonSerializer.Serialize(userSession);
            await SecureStorage.Default.SetAsync(USER_SESSION_KEY, userSessionJson);
        }

        public void RemoveUserSession()
        {
            SecureStorage.Remove(USER_SESSION_KEY);
        }
    }
}
