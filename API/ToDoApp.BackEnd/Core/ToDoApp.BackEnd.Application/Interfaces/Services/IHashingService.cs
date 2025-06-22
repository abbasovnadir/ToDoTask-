namespace ToDoApp.BackEnd.Application.Interfaces.Services.AuthServices;
public interface IHashingService
{
    void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
}

