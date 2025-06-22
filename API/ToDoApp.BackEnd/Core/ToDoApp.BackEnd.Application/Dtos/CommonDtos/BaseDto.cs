using ToDoApp.BackEnd.Application.Dtos.CommonDtos.Interfaces;

namespace ToDoApp.BackEnd.Application.Dtos.CommonDtos;
public record BaseDto : IDto
{
    public int ID { get; set; }

    public bool RowStatus { get; set; }

    public DateTime CreatedAt { get; set; }
    public string CreatedUser { get; set; }

    public DateTime UpdatedAt { get; set; }
    public string UpdatedUser { get; set; }
}
