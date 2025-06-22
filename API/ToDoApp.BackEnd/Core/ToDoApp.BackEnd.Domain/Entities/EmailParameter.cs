using ToDoApp.BackEnd.Domain.Entities.Common;

namespace ToDoApp.BackEnd.Domain.Entities.ApplicationEntities
{
    public class EmailParameter : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string SMTP { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
        public int EmailParameterType { get; set; }
    }
}
