namespace ToDoApp.BackEnd.Application.Dtos.UserLoginDtos.JWTDto
{
    public sealed class TokenPayloadDto
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public DateTime Expiration { get; set; }
    }
}
