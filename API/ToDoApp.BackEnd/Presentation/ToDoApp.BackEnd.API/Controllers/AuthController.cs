using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoApp.BackEnd.API.ApiDtos.AuthControllerDtos;
using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos;
using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos.JWTDto;
using ToDoApp.BackEnd.Application.Interfaces.Services;
using ToDoApp.BackEnd.Application.Interfaces.Services.AuthServices;
using ToDoApp.BackEnd.Application.Utilities.Enums;

namespace ToDoApp.BackEnd.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ITokenService tokenService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _tokenService = tokenService;
            _logger = logger;
        }

        // Login Endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto loginDto)
        {
            var userResult = await _authService.Login(loginDto);

            if (!userResult.IsSuccess)
                return Unauthorized(new { message = userResult.Message });

            var user = userResult.Data;
            var userRolesResult = await _authService.GetUserById(user.Id);

            if (!userRolesResult.IsSuccess)
                return Unauthorized(new { message = userRolesResult.Message });

            var tokens = _tokenService.GenerateTokens(userRolesResult.Data);

            await _tokenService.CreateRefreshToken(user.Id, tokens);
            _logger.LogInformation("Login success.");
            return Ok(new TokenResponceDto(tokens.AccessToken, tokens.AccessTokenExpiration, string.Empty, DateTime.UtcNow));
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register(AppUserCreateDto applicationUserCreateDto)
        {
            var regiosterResult = await _authService.Register(applicationUserCreateDto);
            if (!regiosterResult.IsSuccess)
                return BadRequest(new { message = regiosterResult.Message });

            _logger.LogInformation("Register success.");
            return Ok(regiosterResult);
        }

        [HttpGet("sendConfirmEmail{email}")]
        public async Task<IActionResult> SendConfirmEmail(string email)
        {
            var result = await _authService.SendConfirmEmail(email);
            if (!result.IsSuccess)
                return BadRequest(result);

            _logger.LogInformation("SendConfirmEmail success.");
            return Ok(result);
        }

        [HttpPost("confirmUserValue")]
        public async Task<IActionResult> ConfirmUserValue(EmailConfirmDto dto)
        {
            var result = await _authService.UserEmailConfirm(dto);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            _logger.LogInformation("ConfirmUserValue success.");
            return Ok(result.Message);

        }

        // Refresh Token Endpoint
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] TokenRequestDto tokenRequest)
        {
            var principal = _tokenService.ValidateToken(tokenRequest.RefreshToken);
            if (principal == null)
                return Unauthorized(new { message = "Invalid refresh token" });

            var userId = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized(new { message = "Invalid refresh token" });

            var userResult = await _authService.GetUserById(Guid.Parse(userId));
            if (!userResult.IsSuccess)
                return Unauthorized(new { message = userResult.Message });

            var tokens = _tokenService.GenerateTokens(userResult.Data);

            _tokenService.ValidateToken(tokens.AccessToken);

            _logger.LogInformation("RefreshToken success.");
            return Ok(tokens);
        }

        [HttpPost("sendEmailChangePasswordValue")]
        public async Task<IActionResult> SendEmailChangePasswordValue([FromBody] SendEmailChangePasswordValueDto dto)
        {
            var result = await _authService.SendEmailForChangePassword(dto.UserEmail, EmailTemporaryValueTypes.ChangePassword, EmailTemplateTypes.ChangePassword);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            _logger.LogInformation("SendEmailChangePasswordValue success.");

            return Ok(result);
        }

        [HttpPost("sendEmailForgotPasswordValue")]
        public async Task<IActionResult> SendEmailForgotPasswordValue(SendEmailForgotPasswordValueDto dto)
        {
            var result = await _authService.SendEmailForChangePassword(dto.UserEmail, EmailTemporaryValueTypes.ResetPassword, EmailTemplateTypes.ResetPassword);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            _logger.LogInformation("sendEmailForgotPasswordValue success.");

            return Ok(result);
        }

        [HttpPut("changePassword")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto dto)
        {
            var result = await _authService.ChangePasword(dto.UserEmail, dto.NewPassword, dto.SecretKey);

            if (!result.IsSuccess)
                return BadRequest(result.Message);

            _logger.LogInformation("ChangePassword success.");

            return Ok(result);
        }

        [HttpGet("checkIsConfirmMail")]
        public async Task<IActionResult> CheckIsConfirmMail(string userEmail)
        {
            var result = await _authService.CheckIsConfirmMail(userEmail);
            if (!result.IsSuccess)
                return BadRequest(result.Message);

            return Ok(result);

        }
    }
}
