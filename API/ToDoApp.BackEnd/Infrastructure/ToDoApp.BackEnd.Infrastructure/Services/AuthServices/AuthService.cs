using AutoMapper;
using System.Collections.Generic;
using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos;
using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos.JWTDto;
using ToDoApp.BackEnd.Application.Interfaces.Services;
using ToDoApp.BackEnd.Application.Interfaces.Services.ApplicationServices;
using ToDoApp.BackEnd.Application.Interfaces.Services.AuthServices;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.SuccessResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Domain.Entities;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;
using ToDoApp.BackEnd.Domain.Entities.UserLoginEntities;

namespace ToDoApp.BackEnd.Infrastructure.Services.AuthServices;
public class AuthService : IAuthService
{
    private readonly IHashingService _hashingService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IEmailServices _emailServices;

    public AuthService(IHashingService hashingService, IUnitOfWork unitOfWork, IMapper mapper, IEmailServices emailServices)
    {
        _hashingService = hashingService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _emailServices = emailServices;
    }

    public async Task<IDataResult<CheckUserResponseDto>> GetUserById(Guid userId)
    {
        try
        {
            var user = await _unitOfWork.UserReadRepository<AppUser>().GetFilterAsync(x => x.Id == userId);
            if (user == null)
            {
                return new ErrorDataResult<CheckUserResponseDto>(ResponseTypes.NotFound, "User not found");
            }

            var roles = await _unitOfWork.UserReadRepository<AppUserRoles>()
                .GetAllAsync(x => x.UserId == userId);

            if (!roles.Any())
            {
                return new ErrorDataResult<CheckUserResponseDto>(ResponseTypes.NotFound, "User roles can not be found");
            }

            var roleIds = roles.Select(x => x.RoleId).ToArray();

            var userRoles = new List<AppRole>();
            foreach (var item in roleIds)
            {
                var role = await _unitOfWork.UserReadRepository<AppRole>()
                  .GetFilterAsync(x => x.Id == item);
                userRoles.Add(role);
            }

            var result = new CheckUserResponseDto
            {
                Id = user.Id,
                IsExist = true,
                Role = userRoles.Select(x => x.Name).ToArray(),
                Username = user.Name
            };

            return new SuccessDataResult<CheckUserResponseDto>(result, ResponseTypes.Success);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<CheckUserResponseDto>(ResponseTypes.Exception, ex.Message);
        }
    }

    public async Task<IDataResult<AppUser>> Login(UserForLoginDto userForLoginDto)
    {
        try
        {
            var userToCheck = await _unitOfWork.UserReadRepository<AppUser>().GetFilterAsync(x => x.Email == userForLoginDto.Email);
            if (userToCheck is null)
                return new ErrorDataResult<AppUser>(ResponseTypes.NotFound, $"User not found");
            if (!_hashingService.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PaswordHash, userToCheck.PaswordSalt))
                return new ErrorDataResult<AppUser>(ResponseTypes.Invalid, $"Email or password invalid");

            return new SuccessDataResult<AppUser>(userToCheck, ResponseTypes.Success);
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<AppUser>(ResponseTypes.Exception, ex.Message + "\n" + ex.InnerException);
        }
    }
    public async Task<IResult> CheckIsConfirmMail(string userEmail)
    {
        try
        {
            var user = await _unitOfWork.UserReadRepository<AppUser>()
                                         .GetFilterAsync(x => x.Email == userEmail);

            if (user is null)
                return new ErrorResult(ResponseTypes.NotFound, $"User with email '{userEmail}' not found.");

            if (!user.MailConfirm)
                return new ErrorResult(ResponseTypes.NotActivated, $"Email for user '{user.Email}' is not confirmed.");

            if (!user.IsActive)
                return new ErrorResult(ResponseTypes.NotActivated, $"User profile '{user.Email}' is not active.");

            return new SuccessResult(ResponseTypes.Success,"User is confirmed and active.");
        }
        catch (Exception ex)
        {
            var errorMessage = ex.Message;
            if (ex.InnerException != null)
                errorMessage += "\n" + ex.InnerException.Message;

            return new ErrorResult(ResponseTypes.Exception, errorMessage);
        }
    }


    public async Task<IDataResult<AppUserDto>> Register(AppUserCreateDto applicationUserCreateDto)
    {
        try
        {
            var checkDublicateUser = await _unitOfWork.UserReadRepository<AppUser>().AnyAsync(x => x.Email == applicationUserCreateDto.Email);
            if (checkDublicateUser)
                return new ErrorDataResult<AppUserDto>(ResponseTypes.ExistData, "Email already exist");

            #region create HasPassword
            byte[] passwordHash, passwordSalt;
            _hashingService.CreatePasswordHash(applicationUserCreateDto.Password, out passwordHash, out passwordSalt);
            #endregion

            #region userEntity
            var user = new AppUser()
            {
                Name = applicationUserCreateDto.Name,
                Email = applicationUserCreateDto.Email,
                IsActive = true,
                CreatedAt = DateTime.Now,
                MailConfirm = false,
                MailConfirmDate = DateTime.Now,
                MailConfirmValue = GetRandom6Digit(),
                PaswordHash = passwordHash,
                PaswordSalt = passwordSalt
            };
            #endregion


            _unitOfWork.BeginTransaction();

            try
            {
                #region save user information
                await _unitOfWork.UserWriteRepository<AppUser>().AddAsync(user);
                await _unitOfWork.SaveChangesAsync();

                var userRole = await _unitOfWork.UserReadRepository<AppRole>().GetFilterAsync(x => x.Name == "User");
                if (userRole == null)
                    return new ErrorDataResult<AppUserDto>(ResponseTypes.NotFound, "USER role not found");

                var appuserRole = new AppUserRoles()
                {
                    UserId = user.Id,
                    RoleId = userRole.Id
                };
                await _unitOfWork.UserWriteRepository<AppUserRoles>().AddAsync(appuserRole);
                await _unitOfWork.SaveChangesAsync();


                await _unitOfWork.CommitTransactionAsync();

                var resultData = new AppUserDto()
                {
                    Name = user.Name,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    MailConfirm = user.MailConfirm,
                    MailConfirmValue = user.MailConfirmValue,
                    RoleName = userRole.Name
                };
                #endregion

                #region send email
                await SendConfirmEmail(user, EmailTemplateTypes.Register);

                user.MailConfirmDate = DateTime.Now;

                _unitOfWork.UserWriteRepository<AppUser>().Update(x => x.Id == user.Id, user);
                #endregion

                return new SuccessDataResult<AppUserDto>(resultData, ResponseTypes.Success, "Pleace confirm email");

            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();

                return new ErrorDataResult<AppUserDto>(ResponseTypes.Exception, ex.Message + "\n" + ex.InnerException);
            }
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new ErrorDataResult<AppUserDto>(ResponseTypes.Exception, ex.Message + "\n" + ex.InnerException);
        }
    }

    public async Task<IResult> ChangePasword(string email, string newPassword, string secretKey)
    {
        try
        {
            var user = await _unitOfWork.UserReadRepository<AppUser>().GetFilterAsync(x => x.Email == email);
            if (user == null)
                return new ErrorResult(ResponseTypes.NotFound, "User with the specified email not found.");

            var getUserTempData = await _unitOfWork.UserReadRepository<UserEmailTemporaryValue>().GetFilterAsync(x => x.UserId == user.Id && x.ConfirmValue.Trim() == secretKey.Trim() && x.ConfirmValueType == (int)EmailTemporaryValueTypes.ChangePassword);
            if (getUserTempData is null)
                return new ErrorResult(ResponseTypes.NotFound, "The provided secret key could not be found.");

            if (getUserTempData.ValueExpiredDate < DateTime.UtcNow)
                return new ErrorResult(ResponseTypes.Cancelled, "The secret key has expired. Please request a new one.");

            if (getUserTempData.IsConfirmed)
                return new ErrorResult(ResponseTypes.Cancelled, "The secret key has already been confirmed.");

            _unitOfWork.BeginTransaction();
            try
            {
                #region create HasPassword
                byte[] passwordHash, passwordSalt;
                _hashingService.CreatePasswordHash(newPassword, out passwordHash, out passwordSalt);
                #endregion

                user.PaswordHash = passwordHash;
                user.PaswordSalt = passwordSalt;
                _unitOfWork.UserWriteRepository<AppUser>().Update(x => x.Id == user.Id, user);

                getUserTempData.IsConfirmed = true;
                _unitOfWork.UserWriteRepository<UserEmailTemporaryValue>().Update(x => x.Id == getUserTempData.Id, getUserTempData);

                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return new ErrorResult(ResponseTypes.Exception, ex.Message);
            }

            return new SuccessResult(ResponseTypes.Success);
        }
        catch (Exception ex)
        {
            return new ErrorResult(ResponseTypes.Exception, ex.Message);
        }
    }

    public async Task<IResult> SendConfirmEmail(string email)
    {
        try
        {
            var user = await _unitOfWork.UserReadRepository<AppUser>().GetFilterAsync(x => x.Email == email);
            if (user is null)
                return new ErrorDataResult<AppUserDto>(ResponseTypes.NotFound, "User cannot be found");

            await SendConfirmEmail(user, EmailTemplateTypes.Register);

            user.MailConfirmDate = DateTime.Now;

            _unitOfWork.UserWriteRepository<AppUser>().Update(x => x.Id == user.Id, user);

            return new SuccessDataResult<AppUser>(ResponseTypes.Success);
        }
        catch (Exception ex)
        {
            return new ErrorResult(ResponseTypes.Exception, ex.Message + "\n" + ex.InnerException);
        }
    }

    public async Task<IResult> UserEmailConfirm(EmailConfirmDto dto)
    {
        try
        {
            var user = await _unitOfWork.UserReadRepository<AppUser>().GetFilterAsync(x => x.MailConfirmValue == dto.Value && x.Email == dto.Email);
            if (user is null)
                return new ErrorResult(ResponseTypes.NotFound, "User can not be found");

            user.MailConfirm = true;
            user.MailConfirmDate = DateTime.Now;
            _unitOfWork.UserWriteRepository<AppUser>().Update(x => x.Id == user.Id, user);
            await _unitOfWork.SaveChangesAsync();
            return new SuccessResult(ResponseTypes.Success, $"Confirmed");
        }
        catch (Exception ex)
        {
            return new ErrorResult(ResponseTypes.Exception, ex.Message + "\n" + ex.InnerException);
        }
    }

    private async Task SendConfirmEmail(AppUser user, EmailTemplateTypes templateTypes)
    {
        string subject = "Confirm you accaunt";
        string body = "Pleace. For confirmation click to link [ " + user.MailConfirmValue + " ] ";
        string link = "http://localhost:5138/api/Auth/confirmUser?value=" + user.MailConfirmValue;
        var emailtemplate = await _unitOfWork.ReadRepository<EmailTemplate>().GetFilterAsync(x => x.Type == (int)templateTypes && x.RowStatus == true);
        string linkDecription = "Click me";
        string templateBody = emailtemplate != null ? templateBody = emailtemplate.Template : string.Empty;


        if (!string.IsNullOrEmpty(templateBody))
        {
            templateBody = templateBody.Replace("{{title}}", subject);
            templateBody = templateBody.Replace("{{message}}", body);
            templateBody = templateBody.Replace("{{linkDescription}}", linkDecription);
            templateBody = templateBody.Replace("{{link}}", link);
        }
        var result = await _unitOfWork.ReadRepository<EmailParameter>().GetFilterAsync(x => x.EmailParameterType == (int)EmailParameterTypes.System && x.RowStatus == true);

        if (result != null)
        {
            SendMailDto sendMailDto = new SendMailDto()
            {
                MailParameters = result,
                senderEmail = user.Email,
                subject = subject,
                body = templateBody
            };
            _emailServices.SendEmail(sendMailDto);
        }
    }

    public async Task<IResult> SendEmailForChangePassword(string email, EmailTemporaryValueTypes valueType, EmailTemplateTypes templateTypes)
    {
        var user = await _unitOfWork.UserReadRepository<AppUser>().GetFilterAsync(x => x.Email == email);
        if (user is null)
            return new ErrorResult(ResponseTypes.NotFound, "User with the specified email not found.");

        var secretKey = GetRandom6Digit();

        var data = new UserEmailTemporaryValue()
        {
            ConfirmValue = secretKey,
            ConfirmValueType = (int)valueType,
            ValueExpiredDate = DateTime.UtcNow.AddMinutes(16),
            IsConfirmed = false,
            CreatedAt = DateTime.Now,
            UserId = user.Id
        };

        string subject = "Password Reset Request - Action Required";

        var emailtemplate = await _unitOfWork.ReadRepository<EmailTemplate>().GetFilterAsync(x => x.Type == (int)templateTypes && x.RowStatus == true);

        string templateBody = emailtemplate != null ? templateBody = emailtemplate.Template : string.Empty;

        if (!string.IsNullOrEmpty(templateBody))
        {
            templateBody = templateBody.Replace("{{secretKey}}", secretKey);
        }
        var result = await _unitOfWork.ReadRepository<EmailParameter>().GetFilterAsync(x => x.EmailParameterType == (int)EmailParameterTypes.System && x.RowStatus == true);

        if (result is null)
            return new ErrorResult(ResponseTypes.NotFound, $"EmailParameterType for {templateTypes.ToString()} not found");

        var getUserTempData = await _unitOfWork.UserReadRepository<UserEmailTemporaryValue>().GetFilterAsync(x => x.UserId == user.Id && x.ConfirmValueType == (int)EmailTemporaryValueTypes.ChangePassword);



        _unitOfWork.BeginTransaction();
        try
        {
          

            if (getUserTempData is not null)
            {
                _unitOfWork.UserWriteRepository<UserEmailTemporaryValue>().DeleteAsync(getUserTempData);
            }

            await _unitOfWork.UserWriteRepository<UserEmailTemporaryValue>().AddAsync(data);
            await _unitOfWork.SaveChangesAsync();
            await _unitOfWork.CommitTransactionAsync();

            SendMailDto sendMailDto = new SendMailDto()
            {
                MailParameters = result,
                senderEmail = user.Email,
                subject = subject,
                body = templateBody
            };
            _emailServices.SendEmail(sendMailDto);


        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new ErrorResult(ResponseTypes.Exception, ex.Message);
        }

        return new SuccessResult(ResponseTypes.Success);
    }

    public async Task<IResult> UpdateUserInformation(AppUser user)
    {
        _unitOfWork.UserWriteRepository<AppUser>().Update(x => x.Id == user.Id, user);
        await _unitOfWork.SaveChangesAsync();
        return new SuccessResult(ResponseTypes.Success);
    }

    private string GetRandom6Digit()
    {
        string result = "989009";
        try
        {
            var random = new Random();
            result = random.Next(100000, 999999).ToString();
            return result;
        }
        catch (Exception)
        {
            return result;
        }
    }
}
