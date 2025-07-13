namespace ToDoApp.BackEnd.Infrastructure.Tools.JWTTools;
public class JwtDefaults
{  
    public const string ValidAudience = "https://freetestApi.bsite.net";
    public const string ValidIssuer = "https://freetestApi.bsite.net";
    public const string Key = "G6!9gB*vK2#1@zL&xNQaM$r7pY%4^s5Jh0";
    public const int ExpiredInMinutes = 15;
    public const int RefreshTokenExpiredInDays = 7;
    public const bool ValidateIssuerSigningKey = true;
    public const bool ValidateLifetime = true;

}
