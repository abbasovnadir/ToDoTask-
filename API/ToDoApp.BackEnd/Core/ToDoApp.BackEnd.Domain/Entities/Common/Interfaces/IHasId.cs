namespace ToDoApp.BackEnd.Domain.Entities.Common;
public interface IHasId<Tkey>
{
    public Tkey ID { get; set; }
}
