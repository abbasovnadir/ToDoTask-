namespace ToDoApp.BackEnd.Application.Dtos.UserLoginDtos.JWTDto;
public sealed class RefreshTokenCreateDto
{
    public RefreshTokenCreateDto(Guid userId, string refreshToken, DateTime tokenExpiredDate)
    {
        UserId = userId;
        RefreshToken = refreshToken;
        TokenExpiredDate = tokenExpiredDate;
    }

    public Guid UserId { get; set; }
    public string RefreshToken { get; set; }
    public DateTime TokenExpiredDate { get; set; }
}
