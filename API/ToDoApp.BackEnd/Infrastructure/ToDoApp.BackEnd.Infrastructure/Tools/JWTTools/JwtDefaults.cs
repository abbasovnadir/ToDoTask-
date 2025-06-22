namespace ToDoApp.BackEnd.Infrastructure.Tools.JWTTools;
public class JwtDefaults
{  
    public const string ValidAudience =  "http://localhost";
    public const string ValidIssuer = "http://localhost";
    public const string Key = "G6!9gB*vK2#1@zL&xNQaM$r7pY%4^s5Jh0";
    public const int ExpiredInMinutes = 15;
    public const int RefreshTokenExpiredInDays = 7;
    public const bool ValidateIssuerSigningKey = true;
    public const bool ValidateLifetime = true;

}
