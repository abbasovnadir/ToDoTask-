using ToDoApp.Mobile.App.Models;

namespace ToDoApp.Mobile.App.Services.Interfaces;
internal interface ICustomAuthenticationStateProvider
{
    Task<AccountResponseModel> LoginAsync(UserForLoginDto loginDto, bool rememberMe);
    Task UpdateAuthenticationState(UserSession userSession, bool rememberUser);
    Task<AccountResponseModel> Register(CreateRegisterModel registerModel);
    Task<bool> ConfirmUserValue(EmailConfrmModel model);
    Task<AccountResponseModel> SendChangePasswordEmail(SendEmailChangePasswordDto dto);
    Task<AccountResponseModel> ChangePassword(ChangePasswordDto dto);
}
