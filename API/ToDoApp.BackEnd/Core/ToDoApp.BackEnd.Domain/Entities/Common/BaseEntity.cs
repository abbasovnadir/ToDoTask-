using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.BackEnd.Domain.Entities.Common;
public class BaseEntity : IEntity, IHasId<int>, IHasRowStatus, IHasCreatedInfo, IHasUpdatedInfo
{
    public int ID { get; set; }

    [Required]
    public bool RowStatus { get; set; }

    [Required]
    [Column(TypeName = "datetime")]
    public DateTime CreatedAt { get; set; }

    [Required]
    [MaxLength(128)]
    public string CreatedUser { get; set; }


    [Column(TypeName = "datetime")]
    public DateTime? UpdatedAt { get; set; }


    [MaxLength(128)]
    public string UpdatedUser { get; set; }
}
