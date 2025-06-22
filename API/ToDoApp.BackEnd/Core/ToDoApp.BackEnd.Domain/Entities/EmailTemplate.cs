using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Domain.Entities.ApplicationEntities
{
    public class EmailTemplate : BaseEntity
    {
        public int Type { get; set; }
        public string TypeName { get; set; }
        public string Template { get; set; }
    }
}
