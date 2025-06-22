namespace ToDoApp.BackEnd.API.ApiDtos.AuthControllerDtos;
public class TokenRequestDto
{
    /// <summary>
    /// Userden gelen Refresh Token.
    /// </summary>
    public string RefreshToken { get; set; }

    /// <summary>
    /// Buna o qederde ehtiyyac yoxdur c
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// Bunada elede ehtiyyac yoxdur
    /// </summary>
    public Guid? UserId { get; set; }
}
