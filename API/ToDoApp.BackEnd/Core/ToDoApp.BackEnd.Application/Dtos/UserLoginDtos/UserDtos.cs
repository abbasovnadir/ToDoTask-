namespace ToDoApp.BackEnd.Application.Dtos.UserLoginDtos;

public record UserLoginDtos(string Username, string Password);
public record UserRegisterDtos(string Username, string FullName, string Password, string ConfirmPassword);