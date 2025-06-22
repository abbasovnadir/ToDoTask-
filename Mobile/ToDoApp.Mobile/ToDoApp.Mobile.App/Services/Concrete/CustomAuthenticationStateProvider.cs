using ToDoApp.Mobile.App.Models;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Json;
using System.Text.Json;
using ToDoApp.Mobile.App.Services.Interfaces;

namespace ToDoApp.Mobile.App.Services.Concrete
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider, ICustomAuthenticationStateProvider
    {
        private readonly AuthenticationService _authenticationService;
        private readonly ClaimsPrincipal _anonymus = new ClaimsPrincipal(new ClaimsIdentity());
        private readonly HttpClient _http;
        public CustomAuthenticationStateProvider(AuthenticationService authenticationService, HttpClient http)
        {
            _authenticationService = authenticationService;
            _http = http;
        }

        public async Task<AccountResponseModel> ChangePassword(ChangePasswordDto dto)
        {
            try
            {
                var content = new StringContent(JsonSerializer.Serialize(dto), System.Text.Encoding.UTF8, "application/json");

                var response = await _http.PutAsync("/api/Auth/changePassword", content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                    string message = errorContent?["message"] ?? "An unknown error occurred.";
                    return new AccountResponseModel() { IsSuccess = false, Message = message };
                }

                return new AccountResponseModel() { IsSuccess = true, Message = string.Empty };
            }
            catch (Exception ex)
            {
                return new AccountResponseModel() { IsSuccess = false, Message = ex.Message };
            }
        }
        public async Task<bool> ConfirmUserValue(EmailConfrmModel model)
        {
            try
            {
                var content = new StringContent(
                   JsonSerializer.Serialize(model),
                   System.Text.Encoding.UTF8,
                   "application/json");

                var response = await _http.PostAsync($"/api/Auth/confirmUserValue", content);
                if (!response.IsSuccessStatusCode)
                    return false;

                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var authenticationState = new AuthenticationState(_anonymus);
            var userSession = await _authenticationService.GetUserSession();

            if (userSession is not null)
            {
                var claimPrincipial = GetClaimsPrincipal(userSession);
                authenticationState = new AuthenticationState(claimPrincipial);
            }

            return authenticationState;
        }

        public async Task<AccountResponseModel> LoginAsync(UserForLoginDto loginDto, bool rememberMe)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("/api/Auth/login", loginDto);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                    string message = errorContent?["message"] ?? "An unknown error occurred.";
                    return new AccountResponseModel() { IsSuccess = false, Message = message };
                }

                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponseDto>();

                if (tokenResponse is null)
                {
                    var message = "An unknown error occurred.";
                    return new AccountResponseModel() { IsSuccess = false, Message = message };
                }

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

                JwtSecurityToken token = handler.ReadJwtToken(tokenResponse.AccessToken);

                var roles = token.Claims
                        .Where(c => c.Type == ClaimTypes.Role || c.Type == "role")
                        .Select(c => c.Value)
                        .ToList();

                var userName = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
                var userId = token.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                await UpdateAuthenticationState(new UserSession(userId, userName, roles, tokenResponse.AccessToken), rememberMe);

                return new AccountResponseModel() { IsSuccess = true, Message = string.Empty };
            }
            catch (Exception ex)
            {
                return new AccountResponseModel() { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<AccountResponseModel> Register(CreateRegisterModel registerModel)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("/api/Auth/register", registerModel);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                    string message = errorContent?["message"] ?? "An unknown error occurred.";
                    return new AccountResponseModel() { IsSuccess = false, Message = message };
                }

                return new AccountResponseModel() { IsSuccess = true, Message = string.Empty };
            }
            catch (Exception ex)
            {
                return new AccountResponseModel() { IsSuccess = false, Message = ex.Message };
            }
        }

        public async Task<AccountResponseModel> SendChangePasswordEmail(SendEmailChangePasswordDto dto)
        {
            try
            {
                var content = new StringContent(
                    JsonSerializer.Serialize(dto),
                    System.Text.Encoding.UTF8,
                   "application/json");
                var response = await _http.PostAsync("/api/Auth/sendEmailChangePasswordValue", content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
                    string message = errorContent?["message"] ?? "An unknown error occurred.";
                    return new AccountResponseModel() { IsSuccess = false, Message = message };
                }

                return new AccountResponseModel() { IsSuccess = true, Message = string.Empty };
            }
            catch (Exception ex)
            {
                return new AccountResponseModel() { IsSuccess = false, Message = ex.Message };
            }
        }


        public async Task UpdateAuthenticationState(UserSession userSession, bool rememberUser)
        {
            try
            {
                ClaimsPrincipal claimsPrincipal = _anonymus;

                if (userSession is not null)
                {
                    if (rememberUser)
                    {
                        await _authenticationService.SaveUserSession(userSession);
                    }

                    claimsPrincipal = GetClaimsPrincipal(userSession);
                }
                else
                {
                    _authenticationService.RemoveUserSession();
                }

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
            }
            catch (Exception)
            {
            }
        }

        private ClaimsPrincipal GetClaimsPrincipal(UserSession userSession)
        {
            try
            {
                List<Claim> claims = new List<Claim>();
                foreach (var item in userSession.Roles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }
                claims.Add(new Claim(ClaimTypes.Name, userSession.UserName));
                claims.Add(new Claim("AccessToken", userSession.AccessToken));

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));

                return claimsPrincipal;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
