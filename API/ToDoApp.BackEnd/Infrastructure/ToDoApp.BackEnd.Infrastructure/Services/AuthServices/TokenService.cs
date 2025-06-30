using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos.JWTDto;
using ToDoApp.BackEnd.Application.Interfaces.Services.AuthServices;
using ToDoApp.BackEnd.Application.Interfaces.UnitOfWork;
using ToDoApp.BackEnd.Application.Utilities.Enums;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.ErrorResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Concretes.SuccessResults;
using ToDoApp.BackEnd.Application.Utilities.Results.Interfaces;
using ToDoApp.BackEnd.Domain.Entities;
using ToDoApp.BackEnd.Infrastructure.Tools.JWTTools;

namespace ToDoApp.BackEnd.Infrastructure.Services.AuthServices;

public class TokenService : ITokenService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<TokenService> _logger;
    public TokenService(IUnitOfWork unitOfWork, ILogger<TokenService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public TokenResponceDto GenerateTokens(CheckUserResponseDto dto)
    {
        try
        {
            var claims = new List<Claim>();

            if (dto.Role.Length > 0)
                foreach (var item in dto.Role)
                    claims.Add(new Claim(ClaimTypes.Role, item));

            if (!string.IsNullOrWhiteSpace(dto.Username))
                claims.Add(new Claim(ClaimTypes.Name, dto.Username));

            claims.Add(new Claim(ClaimTypes.NameIdentifier, dto.Id.ToString()));

            claims.Add(new Claim("MemberSince", dto.MemberSince));
            claims.Add(new Claim(ClaimTypes.Email, dto.Email));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtDefaults.Key));
            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var accessTokenExpiration = DateTime.UtcNow.AddMinutes(JwtDefaults.ExpiredInMinutes);
            var refreshTokenExpiration = DateTime.UtcNow.AddDays(JwtDefaults.RefreshTokenExpiredInDays);

            var accessToken = new JwtSecurityToken(
                issuer: JwtDefaults.ValidIssuer,
                audience: JwtDefaults.ValidAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: accessTokenExpiration,
                signingCredentials: credentials
             );

            var refreshToken = new JwtSecurityToken(
                issuer: JwtDefaults.ValidIssuer,
                audience: JwtDefaults.ValidAudience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: refreshTokenExpiration,
                signingCredentials: credentials
            );

            var accessTokenString = new JwtSecurityTokenHandler().WriteToken(accessToken);
            var refreshTokenString = new JwtSecurityTokenHandler().WriteToken(refreshToken);

            _logger.LogInformation("Token generated");

            return new TokenResponceDto(accessTokenString, accessTokenExpiration, refreshTokenString, refreshTokenExpiration);
        }
        catch (SecurityTokenException ex)
        {
            throw new InvalidOperationException("Token security issue occurred.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred during token generation.", ex);
        }
    }

    public ClaimsPrincipal ValidateToken(string token)
    {
        try
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtDefaults.Key));
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = JwtDefaults.ValidIssuer,
                ValidAudience = JwtDefaults.ValidAudience,
                IssuerSigningKey = securityKey
            };

            var principal = tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);
            if (!(validatedToken is JwtSecurityToken jwtToken) ||
              !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                return null;
            }
            return principal;
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<IResult> CreateRefreshToken(Guid userId, TokenResponceDto tokenResponceDto)
    {
        try
        {
            _unitOfWork.BeginTransaction();
            int a = tokenResponceDto.RefreshToken.Length;
            var chechDublicateToken = await _unitOfWork.UserReadRepository<AppUserToken>().GetFilterAsync(x => x.UserId == userId);
            if (chechDublicateToken != null)
            {
                _unitOfWork.UserWriteRepository<AppUserToken>().DeleteAsync(chechDublicateToken);
                await _unitOfWork.SaveChangesAsync();
            }
            var data = new AppUserToken()
            {
                UserId = userId,
                RefreshToken = tokenResponceDto.RefreshToken,
                TokenExpiredDate = tokenResponceDto.RefreshTokenExpiration,
                CreatedAt = DateTime.Now
            };
            await _unitOfWork.UserWriteRepository<AppUserToken>().AddAsync(data);
            await _unitOfWork.SaveChangesAsync();

            await _unitOfWork.CommitTransactionAsync();

            return new SuccessResult(ResponseTypes.Success, $"RefreshToken created for User={userId}");
        }
        catch (Exception ex)
        {
            await _unitOfWork.RollbackTransactionAsync();
            return new ErrorResult(ResponseTypes.Exception, $"Refresh token save failed! \n ExceptionMessge={ex.Message}\nInnerException={ex.InnerException}");
        }
    }

    public async Task<IDataResult<AppUserToken>> GetActiveRefreshToken(Guid userId)
    {
        try
        {
            var getActiveToken = await _unitOfWork.UserReadRepository<AppUserToken>()
           .GetFilterAsync(x => x.UserId == userId && x.TokenExpiredDate > DateTime.UtcNow);
            if (getActiveToken != null)
                return new SuccessDataResult<AppUserToken>(getActiveToken, ResponseTypes.Success);
            else
                return new ErrorDataResult<AppUserToken>(ResponseTypes.NotFound, $"Refresh token not found");
        }
        catch (Exception ex)
        {
            return new ErrorDataResult<AppUserToken>(ResponseTypes.Exception, $"ExceptionMessge={ex.Message}\nInnerException={ex.InnerException}");
        }
    }

    public TokenPayloadDto ExtractTokenData(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var payload = new TokenPayloadDto();

        payload.UserId = new Guid(jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
        var userIdStr = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        if (!Guid.TryParse(userIdStr, out Guid userId))
        {
            userId = Guid.NewGuid();
        }
        payload.UserId = userId;
        payload.Email = jwtToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
        payload.Roles = jwtToken.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList();
        payload.Expiration = jwtToken.ValidTo;


        return payload;
    }

}
