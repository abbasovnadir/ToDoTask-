namespace ToDoApp.BackEnd.Application.Dtos.UserLoginDtos.JWTDto;
public class TokenResponceDto
{
    public string AccessToken { get; set; }
    public DateTime AccessTokenExpiration { get; set; }
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }

    public TokenResponceDto(string accessToken, DateTime accessTokenExpiration, string refreshToken, DateTime refreshTokenExpiration)
    {
        AccessToken = accessToken;
        AccessTokenExpiration = accessTokenExpiration;
        RefreshToken = refreshToken;
        RefreshTokenExpiration = refreshTokenExpiration;
    }
}
