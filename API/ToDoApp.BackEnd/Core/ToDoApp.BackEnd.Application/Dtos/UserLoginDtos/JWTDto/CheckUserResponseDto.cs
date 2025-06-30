namespace ToDoApp.BackEnd.Application.Dtos.UserLoginDtos.JWTDto;
public class CheckUserResponseDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Username { get; set; }
    public string MemberSince { get; set; }
    public string[] Role { get; set; }
    public bool IsExist { get; set; }

}
