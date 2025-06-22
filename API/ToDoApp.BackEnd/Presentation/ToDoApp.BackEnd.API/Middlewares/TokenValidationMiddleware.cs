using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToDoApp.BackEnd.Application.Dtos.UserLoginDtos.JWTDto;
using ToDoApp.BackEnd.Application.Interfaces.Services;
using ToDoApp.BackEnd.Application.Interfaces.Services.AuthServices;

namespace ToDoApp.BackEnd.API.Middlewares;
public class TokenValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<TokenValidationMiddleware> _logger;


    public TokenValidationMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory, ILogger<TokenValidationMiddleware> logger)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {

            var endpoint = context.GetEndpoint();

            if (endpoint != null)
            {
                var controllerName = endpoint.Metadata
                    .OfType<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>()
                    .FirstOrDefault()?.ControllerName;

                if (string.Equals(controllerName, "Auth", StringComparison.OrdinalIgnoreCase))
                {
                    await _next(context);
                    return;
                }
            }

            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
                    var authService = scope.ServiceProvider.GetRequiredService<IAuthService>();

                    // Check if token structure is valid
                    var jwtTokenHandler = new JwtSecurityTokenHandler();
                    JwtSecurityToken jwtToken = null;

                    try
                    {
                        jwtToken = jwtTokenHandler.ReadJwtToken(token);
                    }
                    catch
                    {
                        // If token is invalid, skip to next middleware
                        await _next(context);
                        return;
                    }

                    // Check if token is expired
                    var tokenExpired = jwtToken.ValidTo < DateTime.UtcNow;

                    if (!tokenExpired)
                    {
                        // If token is still valid, validate and attach claims
                        var claimsPrincipal = tokenService.ValidateToken(token);

                        if (claimsPrincipal != null)
                        {
                            context.User = claimsPrincipal;
                            SavePayloadToContext(context, token, tokenService);
                            await _next(context);
                            return;
                        }
                    }

                    // If token is expired or validation failed, try to refresh the token
                    var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

                    if (!string.IsNullOrEmpty(userId))
                    {
                        var refreshTokenResult = await RefreshToken(Guid.Parse(userId.Trim()), authService, tokenService);

                        if (refreshTokenResult != null)
                        {
                            var resultTokenAddition = await tokenService.CreateRefreshToken(Guid.Parse(userId.Trim()), refreshTokenResult);
                            if (resultTokenAddition.IsSuccess)
                            {
                                // Attach new token to request and validate
                                context.Request.Headers["Authorization"] = $"Bearer {refreshTokenResult.AccessToken}";
                                var claimsPrincipal = tokenService.ValidateToken(refreshTokenResult.AccessToken);
                                SavePayloadToContext(context, refreshTokenResult.AccessToken, tokenService);
                                if (claimsPrincipal != null)
                                {
                                    context.User = claimsPrincipal;
                                }

                                await _next(context);
                                return;
                            }
                            else
                            {
                                _logger.LogError("Token generation failed. Please try again later.");
                                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                                await context.Response.WriteAsync("Token generation failed. Please try again later.");
                                return;
                            }
                        }
                        else
                        {
                            _logger.LogError("Both access and refresh tokens expired.");
                            // Both access and refresh tokens are expired
                            context.Response.StatusCode = 440;
                            await context.Response.WriteAsync("Both access and refresh tokens expired.");
                            return;
                        }
                    }
                    else
                    {
                        _logger.LogError("Invalid token: user ID not found.");
                        // Token does not contain a valid user ID
                        context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                        await context.Response.WriteAsync("Invalid token: user ID not found.");
                        return;
                    }
                }
            }
            else
            {
                _logger.LogError("Token is missing.");
                // Token is missing in the request
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token is missing.");
                return;
            }
        }
        catch (Exception ex)
        {
            _logger.LogError("Unexpected server error.");
            // Catch-all for unexpected errors
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsync("Unexpected server error.");
            Console.WriteLine($"Middleware Error: {ex.Message}");
        }
    }

    // Attempts to refresh the access token using a valid refresh token
    public async Task<TokenResponceDto> RefreshToken(Guid userId, IAuthService authService, ITokenService tokenService)
    {
        var user = await GetUserFromDatabase(userId, authService);

        if (user != null)
        {
            var existingRefreshToken = await tokenService.GetActiveRefreshToken(userId);
            if (existingRefreshToken.IsSuccess)
            {
                if (existingRefreshToken.Data.TokenExpiredDate > DateTime.UtcNow)
                {
                    var newTokens = tokenService.GenerateTokens(user);
                    return newTokens;
                }
            }
        }

        return null;
    }

    // Gets user from the database using AuthService
    private async Task<CheckUserResponseDto> GetUserFromDatabase(Guid userId, IAuthService authService)
    {
        var result = await authService.GetUserById(userId);
        return result.Data;
    }

    private void SavePayloadToContext(HttpContext context, string token, ITokenService tokenService)
    {
        var tokenPayload = tokenService.ExtractTokenData(token);
        context.Items["TokenPayload"] = tokenPayload;
    }
}
