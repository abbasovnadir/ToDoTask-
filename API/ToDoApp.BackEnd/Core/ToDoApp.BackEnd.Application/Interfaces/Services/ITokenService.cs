using System.Security.Claims;
using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos.JWTDto;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Domain.Entities;

namespace ToDoApp.BackEnd.Application.Interfaces.Services.AuthServices;
public interface ITokenService
{
    TokenResponceDto GenerateTokens(CheckUserResponseDto dto);
    ClaimsPrincipal ValidateToken(string token);

    Task<IResult> CreateRefreshToken(Guid userId, TokenResponceDto tokenResponceDto);
    Task<IDataResult<AppUserToken>> GetActiveRefreshToken(Guid userId);
    TokenPayloadDto ExtractTokenData(string token);
}
