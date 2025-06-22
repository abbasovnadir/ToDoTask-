using ToDoApp.BackEnd.Application.Dtos.CommonDtos.Interfaces;
using ToDoApp.BackEnd.Domain.Entities.ApplicationEntities;

namespace ToDoApp.BackEnd.Application.Dtos.ApplicationDtos;
public sealed class SendMailDto : IDto
{
    public EmailParameter MailParameters { get; set; }
    public string senderEmail { get; set; }
    public string subject { get; set; }
    public string body { get; set; }
}
