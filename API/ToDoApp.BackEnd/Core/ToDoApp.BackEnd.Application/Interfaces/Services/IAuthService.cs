using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos;
using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos.JWTDto;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Domain.Entities;

namespace ToDoApp.BackEnd.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<IDataResult<AppUserDto>> Register(AppUserCreateDto applicationUserCreateDto);
        Task<IDataResult<AppUser>> Login(UserForLoginDto userForLoginDto);
        Task<IResult> CheckIsConfirmMail(string userEmail);
        Task<IDataResult<CheckUserResponseDto>> GetUserById(Guid userId);
        Task<IResult> UpdateUserInformation(AppUser user);
        Task<IResult> UserEmailConfirm(EmailConfirmDto dto);
        Task<IResult> SendConfirmEmail(string email);
        Task<IResult> ChangePasword(string email, string newPassword, string secretKey);

        Task<IResult> SendEmailForChangePassword(string email, EmailTemporaryValueTypes valueType, EmailTemplateTypes templateTypes);
    }
}
