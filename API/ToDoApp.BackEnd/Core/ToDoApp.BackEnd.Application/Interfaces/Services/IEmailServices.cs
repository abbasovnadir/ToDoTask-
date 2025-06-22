using ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;

namespace ToDoApp.BackEnd.Application.Interfaces.Services.ApplicationServices;
public interface IEmailServices
{
    void SendEmail(SendMailDto sendMailDto);
}
